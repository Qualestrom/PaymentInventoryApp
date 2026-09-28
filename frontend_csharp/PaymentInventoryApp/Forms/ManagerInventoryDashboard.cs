using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using PaymentInventoryApp.Models;
using PaymentInventoryApp.Services;

namespace PaymentInventoryApp.Forms
{
    public partial class ManagerInventoryDashboard : UserControl
    {
        private PythonBridge _bridge;
        private User? _currentUser;
        private List<Product> _cachedProducts = new();

        public ManagerInventoryDashboard()
        {
            InitializeComponent();
            _bridge = new PythonBridge();
            dtpDeliveryDate.Value = DateTime.Today;
            dtpExpiryDate.Value = DateTime.Today.AddDays(30);
        }

        public void Initialize(User user, PythonBridge bridge)
        {
            _currentUser = user;
            _bridge = bridge;
            lblSubtitle.Text = $"Active Manager: {user.FullName} ({user.Role}) • Synchronized Data Tier: PostgreSQL Batches (store_inventory) • MySQL Master Catalog (store_sales)";

            LoadAllData();
        }

        public void LoadAllData()
        {
            LoadProducts();
            LoadBatches();
            LoadAlerts();
        }

        private void BtnRefreshAll_Click(object? sender, EventArgs e)
        {
            LoadAllData();
            MessageBox.Show("Inventory batches and catalog data refreshed successfully from dual databases.", "Data Synchronized", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TabManager_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabManager.SelectedTab == tabBatches)
            {
                LoadBatches();
            }
            else if (tabManager.SelectedTab == tabAlerts)
            {
                LoadAlerts();
            }
            else if (tabManager.SelectedTab == tabIntake || tabManager.SelectedTab == tabPrice)
            {
                LoadProducts();
            }
        }

        // =====================================================================
        // Data Loaders
        // =====================================================================

        private void LoadProducts()
        {
            var (success, prods, error) = _bridge.GetAllProducts();
            if (success)
            {
                _cachedProducts = prods;

                // Populate Intake ComboBox
                cboIntakeProduct.DataSource = null;
                cboIntakeProduct.DisplayMember = "Name";
                cboIntakeProduct.ValueMember = "ProductId";
                cboIntakeProduct.DataSource = new List<Product>(_cachedProducts);

                // Populate Price ComboBox
                cboPriceProduct.DataSource = null;
                cboPriceProduct.DisplayMember = "Name";
                cboPriceProduct.ValueMember = "ProductId";
                cboPriceProduct.DataSource = new List<Product>(_cachedProducts);

                if (_cachedProducts.Count > 0)
                {
                    UpdatePriceDisplay(_cachedProducts[0]);
                }
            }
        }

        private void LoadBatches()
        {
            var (success, batches, error) = _bridge.GetInventory();
            if (success)
            {
                dgvBatches.DataSource = null;
                dgvBatches.DataSource = batches;

                if (dgvBatches.Columns.Contains("BatchId")) dgvBatches.Columns["BatchId"].HeaderText = "Batch #";
                if (dgvBatches.Columns.Contains("ProductId")) dgvBatches.Columns["ProductId"].HeaderText = "SKU ID";
                if (dgvBatches.Columns.Contains("Barcode")) dgvBatches.Columns["Barcode"].HeaderText = "Barcode";
                if (dgvBatches.Columns.Contains("ProductName")) dgvBatches.Columns["ProductName"].HeaderText = "Item Description";
                if (dgvBatches.Columns.Contains("Quantity")) dgvBatches.Columns["Quantity"].HeaderText = "Available Units";
                if (dgvBatches.Columns.Contains("DeliveryDate")) dgvBatches.Columns["DeliveryDate"].HeaderText = "Delivery Date (FIFO Order)";
                if (dgvBatches.Columns.Contains("ExpiryDate")) dgvBatches.Columns["ExpiryDate"].HeaderText = "Expiry Date";
                if (dgvBatches.Columns.Contains("DaysRemaining")) dgvBatches.Columns["DaysRemaining"].Visible = false;
            }
        }

        private void LoadAlerts()
        {
            // 1. Low Stock Alerts
            var (lowSuccess, lowData, lowErr) = _bridge.GetLowStock();
            dgvLowStock.Rows.Clear();
            dgvLowStock.Columns.Clear();
            dgvLowStock.Columns.Add("colLowSku", "SKU");
            dgvLowStock.Columns.Add("colLowName", "Product Description");
            dgvLowStock.Columns.Add("colLowQty", "Total Stock");
            dgvLowStock.Columns.Add("colLowThresh", "Threshold");

            if (lowSuccess && lowData is JsonArray lowArr)
            {
                foreach (var item in lowArr)
                {
                    if (item == null) continue;
                    int rIdx = dgvLowStock.Rows.Add(
                        item["barcode"]?.GetValue<string>() ?? "N/A",
                        item["name"]?.GetValue<string>() ?? "Unknown",
                        item["total_quantity"]?.GetValue<int>() ?? 0,
                        item["threshold"]?.GetValue<int>() ?? 10
                    );
                    var row = dgvLowStock.Rows[rIdx];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                }
            }

            // 2. Expiring Soon Alerts
            var (expSuccess, expData, expErr) = _bridge.GetExpiringSoon();
            dgvExpiring.Rows.Clear();
            dgvExpiring.Columns.Clear();
            dgvExpiring.Columns.Add("colExpBatch", "Batch #");
            dgvExpiring.Columns.Add("colExpName", "Item Description");
            dgvExpiring.Columns.Add("colExpQty", "Stock Left");
            dgvExpiring.Columns.Add("colExpDate", "Expiry Date");
            dgvExpiring.Columns.Add("colExpDays", "Days Left");

            if (expSuccess && expData is JsonArray expArr)
            {
                foreach (var item in expArr)
                {
                    if (item == null) continue;
                    int rIdx = dgvExpiring.Rows.Add(
                        item["batch_id"]?.GetValue<int>() ?? 0,
                        item["name"]?.GetValue<string>() ?? "Unknown",
                        item["quantity"]?.GetValue<int>() ?? 0,
                        item["expiry_date"]?.GetValue<string>() ?? "",
                        $"{item["days_remaining"]?.GetValue<int>() ?? 0} days"
                    );
                    var row = dgvExpiring.Rows[rIdx];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 231);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(211, 84, 0);
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }
        }

        // =====================================================================
        // Stock Intake (Restocking)
        // =====================================================================

        private void BtnSubmitIntake_Click(object? sender, EventArgs e)
        {
            if (cboIntakeProduct.SelectedItem is not Product selectedProd)
            {
                lblIntakeStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblIntakeStatus.Text = "Please select a product.";
                return;
            }

            int qty = (int)numIntakeQty.Value;
            string deliveryDate = dtpDeliveryDate.Value.ToString("yyyy-MM-dd");
            string expiryDate = dtpExpiryDate.Value.ToString("yyyy-MM-dd");

            if (dtpExpiryDate.Value <= dtpDeliveryDate.Value)
            {
                lblIntakeStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblIntakeStatus.Text = "Expiry date must be later than delivery date.";
                return;
            }

            lblIntakeStatus.ForeColor = Color.FromArgb(52, 152, 219);
            lblIntakeStatus.Text = "Recording batch arrival into PostgreSQL inventory...";
            Application.DoEvents();

            var (success, batchId, error) = _bridge.RecordStockArrival(selectedProd.ProductId, qty, deliveryDate, expiryDate);

            if (success)
            {
                lblIntakeStatus.ForeColor = Color.FromArgb(46, 204, 113);
                lblIntakeStatus.Text = $"✔ Batch #{batchId} logged successfully ({qty} units of '{selectedProd.Name}').";
                MessageBox.Show(
                    $"New Stock Batch Recorded!\n\nBatch ID: {batchId}\nProduct: {selectedProd.Name}\nDelivered Qty: {qty}\nDelivery Date: {deliveryDate}\nExpiry Date: {expiryDate}",
                    "Stock Arrival Logged",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                LoadBatches();
                LoadAlerts();
            }
            else
            {
                lblIntakeStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblIntakeStatus.Text = $"Error: {error ?? "Failed to log batch."}";
            }
        }

        // =====================================================================
        // Price Management
        // =====================================================================

        private void CboPriceProduct_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboPriceProduct.SelectedItem is Product prod)
            {
                UpdatePriceDisplay(prod);
            }
        }

        private void UpdatePriceDisplay(Product prod)
        {
            lblCurrentPriceVal.Text = $"PHP {prod.Price:F2}";
            numNewPrice.Value = prod.Price;
            lblPriceStatus.Text = "";
        }

        private void BtnUpdatePrice_Click(object? sender, EventArgs e)
        {
            if (cboPriceProduct.SelectedItem is not Product selectedProd)
            {
                lblPriceStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblPriceStatus.Text = "Please select a product.";
                return;
            }

            // Role authorization check (Manager constraint)
            if (_currentUser == null || !_currentUser.IsManager)
            {
                MessageBox.Show("Security Violation: Only authorized Manager accounts may modify product pricing.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            decimal newPrice = numNewPrice.Value;

            lblPriceStatus.ForeColor = Color.FromArgb(52, 152, 219);
            lblPriceStatus.Text = "Updating product price in MySQL master catalog...";
            Application.DoEvents();

            var (success, msg, error) = _bridge.UpdateProductPrice(selectedProd.ProductId, newPrice, _currentUser.Role);

            if (success)
            {
                selectedProd.Price = newPrice;
                lblCurrentPriceVal.Text = $"PHP {newPrice:F2}";
                lblPriceStatus.ForeColor = Color.FromArgb(46, 204, 113);
                lblPriceStatus.Text = $"✔ {msg}";

                MessageBox.Show(
                    $"Price Updated Successfully!\n\nProduct: {selectedProd.Name}\nNew Price: PHP {newPrice:F2}",
                    "Price Master Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                lblPriceStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblPriceStatus.Text = $"Error: {error ?? "Update failed."}";
            }
        }
    }
}
