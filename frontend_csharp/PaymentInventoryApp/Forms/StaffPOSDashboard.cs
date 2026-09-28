using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PaymentInventoryApp.Models;
using PaymentInventoryApp.Services;

namespace PaymentInventoryApp.Forms
{
    public partial class StaffPOSDashboard : UserControl
    {
        private readonly PythonBridge _bridge;
        private readonly PdfReceiptService _pdfService;
        private User? _currentUser;
        private readonly List<CartItem> _cartItems = new();

        // Sample barcode pool for handheld scanner hardware simulation
        private readonly string[] _sampleBarcodes = new[]
        {
            "4901234567890", // Whole Milk 1L (Near Expiry -> Smart Alert)
            "4902345678901", // White Bread Loaf (Near Expiry -> Smart Alert)
            "4903456789012", // Canned Tuna 155g
            "4904567890123", // Instant Coffee 200g (Low Stock)
            "4905678901234"  // Bottled Water 500mL
        };
        private int _sampleBarcodeIndex = 0;

        // Current calculated totals
        private decimal _currentSubtotal = 0.0m;
        private decimal _currentTaxAmount = 0.0m;
        private decimal _currentDiscountAmount = 0.0m;
        private decimal _currentNetTotal = 0.0m;

        public StaffPOSDashboard()
        {
            InitializeComponent();
            _bridge = new PythonBridge();
            _pdfService = new PdfReceiptService();
            cboDiscountPreset.SelectedIndex = 0; // 0% Standard
        }

        public void Initialize(User user, PythonBridge bridge)
        {
            _currentUser = user;
            lblSubtitle.Text = $"Cashier: {user.FullName} ({user.Role}) • Hardware Simulation: Barcode Scanner Input • PDF Thermal Receipt Generation";
            txtBarcode.Focus();
        }

        // =====================================================================
        // Hardware Simulation: Barcode Scanner
        // =====================================================================

        private void BtnSampleBarcode_Click(object? sender, EventArgs e)
        {
            // Simulate handheld scanner triggering and populating the barcode buffer
            string scannedBarcode = _sampleBarcodes[_sampleBarcodeIndex % _sampleBarcodes.Length];
            _sampleBarcodeIndex++;
            txtBarcode.Text = scannedBarcode;
            txtBarcode.SelectAll();
            AddItemByBarcode(scannedBarcode);
        }

        private void TxtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string barcode = txtBarcode.Text.Trim();
                if (!string.IsNullOrEmpty(barcode))
                {
                    AddItemByBarcode(barcode);
                }
            }
        }

        private void BtnAddItem_Click(object? sender, EventArgs e)
        {
            string barcode = txtBarcode.Text.Trim();
            if (!string.IsNullOrEmpty(barcode))
            {
                AddItemByBarcode(barcode);
            }
        }

        private void AddItemByBarcode(string barcode)
        {
            lblScanStatus.ForeColor = Color.FromArgb(0, 120, 212);
            lblScanStatus.Text = $"Querying product and active inventory batches for barcode: {barcode}...";
            Application.DoEvents();

            var (success, product, error) = _bridge.LookupProduct(barcode);

            if (!success || product == null)
            {
                lblScanStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblScanStatus.Text = $"Scan Error: {error ?? "Item not recognized."}";
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            // Check if already in cart
            var existing = _cartItems.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    Barcode = product.Barcode,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    IsNearExpiry = product.IsNearExpiry,
                    DaysUntilExpiry = product.DaysUntilExpiry
                });
            }

            // Cashier Smart Alert notification
            if (product.IsNearExpiry)
            {
                lblScanStatus.ForeColor = Color.FromArgb(180, 85, 0); // Orange/Amber
                lblScanStatus.Text = $"⚠ SMART EXPIRY ALERT: Earliest batch for '{product.Name}' expires in {product.DaysUntilExpiry} days! Row highlighted in orange.";
                System.Media.SystemSounds.Exclamation.Play();
            }
            else
            {
                lblScanStatus.ForeColor = Color.FromArgb(40, 167, 69);
                lblScanStatus.Text = $"Scanned: '{product.Name}' (PHP {product.Price:F2}). Added to cart.";
            }

            txtBarcode.Clear();
            txtBarcode.Focus();
            RefreshCartGrid();
            RecalculateTotals();
        }

        // =====================================================================
        // Cart Grid & Smart Alert Rendering
        // =====================================================================

        private void RefreshCartGrid()
        {
            dgvCart.Rows.Clear();

            foreach (var item in _cartItems)
            {
                int rowIndex = dgvCart.Rows.Add(
                    item.Barcode,
                    item.ProductName,
                    $"PHP {item.UnitPrice:F2}",
                    item.Quantity,
                    $"PHP {item.LineTotal:F2}",
                    item.ExpiryAlertText
                );

                var row = dgvCart.Rows[rowIndex];
                row.Tag = item;

                // Smart Alert Visual Highlighting:
                // Yellow/Orange background for near-expiry batches to protect customer satisfaction
                if (item.IsNearExpiry)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205); // Warning Amber
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(133, 100, 4);
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void DgvCart_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var item = dgvCart.SelectedRows[0].Tag as CartItem;
                if (item != null)
                {
                    numQty.Value = item.Quantity;
                }
            }
        }

        private void BtnUpdateQty_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var item = dgvCart.SelectedRows[0].Tag as CartItem;
                if (item != null)
                {
                    item.Quantity = (int)numQty.Value;
                    RefreshCartGrid();
                    RecalculateTotals();
                }
            }
        }

        private void BtnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var item = dgvCart.SelectedRows[0].Tag as CartItem;
                if (item != null)
                {
                    _cartItems.Remove(item);
                    RefreshCartGrid();
                    RecalculateTotals();
                }
            }
        }

        private void BtnClearCart_Click(object? sender, EventArgs e)
        {
            if (_cartItems.Count == 0) return;
            if (MessageBox.Show("Are you sure you want to clear the entire cart?", "Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _cartItems.Clear();
                RefreshCartGrid();
                RecalculateTotals();
                lblScanStatus.ForeColor = Color.FromArgb(108, 117, 125);
                lblScanStatus.Text = "Cart cleared.";
            }
        }

        // =====================================================================
        // Pricing, Tax & Discount Calculations
        // =====================================================================

        private void CboDiscountPreset_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (cboDiscountPreset.SelectedIndex)
            {
                case 0: // 0%
                    numDiscount.Value = 0m;
                    numDiscount.Enabled = false;
                    break;
                case 1: // 20% Senior / PWD
                    numDiscount.Value = 20m;
                    numDiscount.Enabled = false;
                    break;
                case 2: // 5% Promo
                    numDiscount.Value = 5m;
                    numDiscount.Enabled = false;
                    break;
                case 3: // 10% Staff
                    numDiscount.Value = 10m;
                    numDiscount.Enabled = false;
                    break;
                case 4: // Custom
                    numDiscount.Enabled = true;
                    break;
            }
            RecalculateTotals();
        }

        private void NumDiscount_ValueChanged(object? sender, EventArgs e)
        {
            RecalculateTotals();
        }

        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            if (_cartItems.Count == 0)
            {
                _currentSubtotal = 0.0m;
                _currentDiscountAmount = 0.0m;
                _currentTaxAmount = 0.0m;
                _currentNetTotal = 0.0m;
                lblSubtotalVal.Text = "PHP 0.00";
                lblTaxVal.Text = "PHP 0.00";
                lblTotalVal.Text = "PHP 0.00";
                return;
            }

            decimal discountPct = numDiscount.Value;
            var (success, data, error) = _bridge.CalculateTransaction(_cartItems, 0.12m, discountPct);

            if (success && data != null)
            {
                _currentSubtotal = data["subtotal"]?.GetValue<decimal>() ?? 0.0m;
                _currentDiscountAmount = data["discount_amount"]?.GetValue<decimal>() ?? 0.0m;
                _currentTaxAmount = data["tax_amount"]?.GetValue<decimal>() ?? 0.0m;
                _currentNetTotal = data["total"]?.GetValue<decimal>() ?? 0.0m;

                lblSubtotalVal.Text = $"PHP {_currentSubtotal:F2}";
                lblTaxVal.Text = $"PHP {_currentTaxAmount:F2}";
                lblTotalVal.Text = $"PHP {_currentNetTotal:F2}";
            }
            else
            {
                // Fallback internal calculation if bridge unreachable
                _currentSubtotal = _cartItems.Sum(i => i.LineTotal);
                _currentDiscountAmount = _currentSubtotal * (discountPct / 100m);
                decimal taxable = Math.Max(0m, _currentSubtotal - _currentDiscountAmount);
                _currentTaxAmount = taxable * 0.12m;
                _currentNetTotal = taxable + _currentTaxAmount;

                lblSubtotalVal.Text = $"PHP {_currentSubtotal:F2}";
                lblTaxVal.Text = $"PHP {_currentTaxAmount:F2}";
                lblTotalVal.Text = $"PHP {_currentNetTotal:F2}";
            }
        }

        // =====================================================================
        // Checkout, FIFO Stock Deduction, and Thermal Receipt Printing
        // =====================================================================

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Cannot checkout with an empty cart. Please scan items first.", "POS Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RecalculateTotals();

            var confirmResult = MessageBox.Show(
                $"Proceed with checkout?\n\nItems in Cart: {_cartItems.Count}\nTotal Amount Due: PHP {_currentNetTotal:F2}",
                "Confirm Payment & Checkout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes) return;

            // 1. First-In, First-Out (FIFO) stock deduction against PostgreSQL batches
            lblScanStatus.ForeColor = Color.FromArgb(0, 120, 212);
            lblScanStatus.Text = "Executing FIFO inventory deduction against warehouse batches...";
            Application.DoEvents();

            var (deductSuccess, deductError) = _bridge.DeductStock(_cartItems);
            if (!deductSuccess)
            {
                MessageBox.Show(
                    $"Checkout Aborted: Stock Deduction Failed!\n\nReason: {deductError}",
                    "FIFO Stock Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                lblScanStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblScanStatus.Text = $"Stock Deduction Error: {deductError}";
                return;
            }

            // 2. Record sale into MySQL sales_transactions
            lblScanStatus.Text = "Recording sales transaction in sales ledger...";
            Application.DoEvents();

            var (saleSuccess, txId, saleError) = _bridge.RecordSale(
                _currentUser?.UserId,
                _currentSubtotal,
                _currentTaxAmount,
                numDiscount.Value,
                _currentNetTotal,
                _cartItems
            );

            if (!saleSuccess)
            {
                MessageBox.Show($"Warning: Inventory deducted but sale recording logged an error: {saleError}", "Sales Ledger Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Hardware Simulation: Thermal Receipt Printing to PDF
            lblScanStatus.Text = "Routing receipt to PDF Thermal Printer simulation...";
            Application.DoEvents();

            var transactionModel = new SaleTransaction
            {
                TransactionId = txId > 0 ? txId : 1001,
                Date = DateTime.Now,
                Subtotal = _currentSubtotal,
                TaxAmount = _currentTaxAmount,
                DiscountPercent = numDiscount.Value,
                TotalAmount = _currentNetTotal,
                CashierId = _currentUser?.UserId,
                CashierName = _currentUser?.FullName ?? "Cashier Staff",
                Items = new List<CartItem>(_cartItems)
            };

            bool printSuccess = _pdfService.PrintReceipt(transactionModel, this);

            string printNote = printSuccess 
                ? "PDF Receipt successfully generated." 
                : "Receipt printing was dismissed or completed.";

            MessageBox.Show(
                $"Transaction Completed Successfully!\n\nReceipt No: TX-{transactionModel.TransactionId:D6}\nTotal Paid: PHP {_currentNetTotal:F2}\nFIFO Deduction: Completed\n\n{printNote}",
                "Checkout Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // 4. Reset Cart
            _cartItems.Clear();
            RefreshCartGrid();
            RecalculateTotals();
            lblScanStatus.ForeColor = Color.FromArgb(40, 167, 69);
            lblScanStatus.Text = "Transaction finished. Ready for next customer.";
            txtBarcode.Focus();
        }
    }
}
