namespace PaymentInventoryApp.Forms
{
    partial class StaffPOSDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlScan = new System.Windows.Forms.Panel();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnSampleBarcode = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lblScanStatus = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCartActions = new System.Windows.Forms.Panel();
            this.lblQtyEdit = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.btnUpdateQty = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblSubtotalTitle = new System.Windows.Forms.Label();
            this.lblSubtotalVal = new System.Windows.Forms.Label();
            this.lblDiscountTitle = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.cboDiscountPreset = new System.Windows.Forms.ComboBox();
            this.lblTaxTitle = new System.Windows.Forms.Label();
            this.lblTaxVal = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotalVal = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlScan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.pnlTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(242)))));
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1080, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(15, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(217, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Staff POS Checkout Hub";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 34);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(433, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Hardware Simulation: Barcode Scanner Input • PDF Thermal Receipt Generation";
            // 
            // pnlScan
            // 
            this.pnlScan.BackColor = System.Drawing.Color.White;
            this.pnlScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlScan.Controls.Add(this.lblBarcode);
            this.pnlScan.Controls.Add(this.txtBarcode);
            this.pnlScan.Controls.Add(this.btnSampleBarcode);
            this.pnlScan.Controls.Add(this.btnAddItem);
            this.pnlScan.Controls.Add(this.lblScanStatus);
            this.pnlScan.Location = new System.Drawing.Point(15, 70);
            this.pnlScan.Name = "pnlScan";
            this.pnlScan.Size = new System.Drawing.Size(685, 80);
            this.pnlScan.TabIndex = 1;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblBarcode.Location = new System.Drawing.Point(10, 8);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(126, 17);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Barcode / SKU Scan";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(10, 28);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PlaceholderText = "Scan or type barcode, then press Enter...";
            this.txtBarcode.Size = new System.Drawing.Size(310, 27);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            // 
            // btnSampleBarcode
            // 
            this.btnSampleBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnSampleBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSampleBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSampleBarcode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSampleBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(242)))));
            this.btnSampleBarcode.Location = new System.Drawing.Point(328, 26);
            this.btnSampleBarcode.Name = "btnSampleBarcode";
            this.btnSampleBarcode.Size = new System.Drawing.Size(220, 31);
            this.btnSampleBarcode.TabIndex = 2;
            this.btnSampleBarcode.Text = "⚡ Sample Barcode (Simulate Scan)";
            this.btnSampleBarcode.UseVisualStyleBackColor = false;
            this.btnSampleBarcode.Click += new System.EventHandler(this.BtnSampleBarcode_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(556, 26);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(115, 31);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "Add to Cart";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            // 
            // lblScanStatus
            // 
            this.lblScanStatus.AutoSize = true;
            this.lblScanStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblScanStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblScanStatus.Location = new System.Drawing.Point(10, 58);
            this.lblScanStatus.Name = "lblScanStatus";
            this.lblScanStatus.Size = new System.Drawing.Size(193, 15);
            this.lblScanStatus.TabIndex = 4;
            this.lblScanStatus.Text = "Ready to scan items into active cart.";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBarcode,
            this.colName,
            this.colPrice,
            this.colQty,
            this.colTotal,
            this.colAlert});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.Location = new System.Drawing.Point(15, 160);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(685, 360);
            this.dgvCart.TabIndex = 2;
            this.dgvCart.SelectionChanged += new System.EventHandler(this.DgvCart_SelectionChanged);
            // 
            // colBarcode
            // 
            this.colBarcode.FillWeight = 85F;
            this.colBarcode.HeaderText = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.FillWeight = 130F;
            this.colName.HeaderText = "Item Description";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.FillWeight = 60F;
            this.colPrice.HeaderText = "Price (PHP)";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colQty
            // 
            this.colQty.FillWeight = 45F;
            this.colQty.HeaderText = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colTotal
            // 
            this.colTotal.FillWeight = 65F;
            this.colTotal.HeaderText = "Total (PHP)";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colAlert
            // 
            this.colAlert.FillWeight = 115F;
            this.colAlert.HeaderText = "Smart Alert";
            this.colAlert.Name = "colAlert";
            this.colAlert.ReadOnly = true;
            // 
            // pnlCartActions
            // 
            this.pnlCartActions.BackColor = System.Drawing.Color.White;
            this.pnlCartActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCartActions.Controls.Add(this.lblQtyEdit);
            this.pnlCartActions.Controls.Add(this.numQty);
            this.pnlCartActions.Controls.Add(this.btnUpdateQty);
            this.pnlCartActions.Controls.Add(this.btnRemoveItem);
            this.pnlCartActions.Controls.Add(this.btnClearCart);
            this.pnlCartActions.Location = new System.Drawing.Point(15, 530);
            this.pnlCartActions.Name = "pnlCartActions";
            this.pnlCartActions.Size = new System.Drawing.Size(685, 55);
            this.pnlCartActions.TabIndex = 3;
            // 
            // lblQtyEdit
            // 
            this.lblQtyEdit.AutoSize = true;
            this.lblQtyEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQtyEdit.Location = new System.Drawing.Point(10, 18);
            this.lblQtyEdit.Name = "lblQtyEdit";
            this.lblQtyEdit.Size = new System.Drawing.Size(76, 15);
            this.lblQtyEdit.TabIndex = 0;
            this.lblQtyEdit.Text = "Adjust Qty:";
            // 
            // numQty
            // 
            this.numQty.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numQty.Location = new System.Drawing.Point(90, 15);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(60, 24);
            this.numQty.TabIndex = 1;
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnUpdateQty
            // 
            this.btnUpdateQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnUpdateQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdateQty.Location = new System.Drawing.Point(160, 13);
            this.btnUpdateQty.Name = "btnUpdateQty";
            this.btnUpdateQty.Size = new System.Drawing.Size(85, 28);
            this.btnUpdateQty.TabIndex = 2;
            this.btnUpdateQty.Text = "Set Quantity";
            this.btnUpdateQty.UseVisualStyleBackColor = false;
            this.btnUpdateQty.Click += new System.EventHandler(this.BtnUpdateQty_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btnRemoveItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(44)))), ((int)(((byte)(13)))));
            this.btnRemoveItem.Location = new System.Drawing.Point(260, 13);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(120, 28);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.White;
            this.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClearCart.Location = new System.Drawing.Point(575, 13);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(95, 28);
            this.btnClearCart.TabIndex = 4;
            this.btnClearCart.Text = "Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.BtnClearCart_Click);
            // 
            // pnlTotals
            // 
            this.pnlTotals.BackColor = System.Drawing.Color.White;
            this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotals.Controls.Add(this.lblSubtotalTitle);
            this.pnlTotals.Controls.Add(this.lblSubtotalVal);
            this.pnlTotals.Controls.Add(this.lblDiscountTitle);
            this.pnlTotals.Controls.Add(this.numDiscount);
            this.pnlTotals.Controls.Add(this.cboDiscountPreset);
            this.pnlTotals.Controls.Add(this.lblTaxTitle);
            this.pnlTotals.Controls.Add(this.lblTaxVal);
            this.pnlTotals.Controls.Add(this.lblTotalTitle);
            this.pnlTotals.Controls.Add(this.lblTotalVal);
            this.pnlTotals.Controls.Add(this.btnCalculate);
            this.pnlTotals.Controls.Add(this.btnCheckout);
            this.pnlTotals.Location = new System.Drawing.Point(715, 70);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(350, 515);
            this.pnlTotals.TabIndex = 4;
            // 
            // lblSubtotalTitle
            // 
            this.lblSubtotalTitle.AutoSize = true;
            this.lblSubtotalTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(115)))), ((int)(((byte)(129)))));
            this.lblSubtotalTitle.Location = new System.Drawing.Point(20, 20);
            this.lblSubtotalTitle.Name = "lblSubtotalTitle";
            this.lblSubtotalTitle.Size = new System.Drawing.Size(63, 19);
            this.lblSubtotalTitle.TabIndex = 0;
            this.lblSubtotalTitle.Text = "Subtotal:";
            // 
            // lblSubtotalVal
            // 
            this.lblSubtotalVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(43)))), ((int)(((byte)(54)))));
            this.lblSubtotalVal.Location = new System.Drawing.Point(150, 18);
            this.lblSubtotalVal.Name = "lblSubtotalVal";
            this.lblSubtotalVal.Size = new System.Drawing.Size(180, 23);
            this.lblSubtotalVal.TabIndex = 1;
            this.lblSubtotalVal.Text = "PHP 0.00";
            this.lblSubtotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountTitle
            // 
            this.lblDiscountTitle.AutoSize = true;
            this.lblDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(115)))), ((int)(((byte)(129)))));
            this.lblDiscountTitle.Location = new System.Drawing.Point(20, 65);
            this.lblDiscountTitle.Name = "lblDiscountTitle";
            this.lblDiscountTitle.Size = new System.Drawing.Size(78, 19);
            this.lblDiscountTitle.TabIndex = 2;
            this.lblDiscountTitle.Text = "Discount %:";
            // 
            // cboDiscountPreset
            // 
            this.cboDiscountPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiscountPreset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboDiscountPreset.FormattingEnabled = true;
            this.cboDiscountPreset.Items.AddRange(new object[] {
            "0% (Standard)",
            "20% (Senior / PWD)",
            "5% (Promo)",
            "10% (Staff)",
            "Custom"});
            this.cboDiscountPreset.Location = new System.Drawing.Point(120, 63);
            this.cboDiscountPreset.Name = "cboDiscountPreset";
            this.cboDiscountPreset.Size = new System.Drawing.Size(130, 23);
            this.cboDiscountPreset.TabIndex = 3;
            this.cboDiscountPreset.SelectedIndexChanged += new System.EventHandler(this.CboDiscountPreset_SelectedIndexChanged);
            // 
            // numDiscount
            // 
            this.numDiscount.DecimalPlaces = 1;
            this.numDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numDiscount.Location = new System.Drawing.Point(260, 63);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(70, 23);
            this.numDiscount.TabIndex = 4;
            this.numDiscount.ValueChanged += new System.EventHandler(this.NumDiscount_ValueChanged);
            // 
            // lblTaxTitle
            // 
            this.lblTaxTitle.AutoSize = true;
            this.lblTaxTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTaxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(115)))), ((int)(((byte)(129)))));
            this.lblTaxTitle.Location = new System.Drawing.Point(20, 110);
            this.lblTaxTitle.Name = "lblTaxTitle";
            this.lblTaxTitle.Size = new System.Drawing.Size(74, 19);
            this.lblTaxTitle.TabIndex = 5;
            this.lblTaxTitle.Text = "VAT (12%):";
            // 
            // lblTaxVal
            // 
            this.lblTaxVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTaxVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(43)))), ((int)(((byte)(54)))));
            this.lblTaxVal.Location = new System.Drawing.Point(150, 108);
            this.lblTaxVal.Name = "lblTaxVal";
            this.lblTaxVal.Size = new System.Drawing.Size(180, 23);
            this.lblTaxVal.TabIndex = 6;
            this.lblTaxVal.Text = "PHP 0.00";
            this.lblTaxVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(43)))), ((int)(((byte)(54)))));
            this.lblTotalTitle.Location = new System.Drawing.Point(20, 175);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(126, 21);
            this.lblTotalTitle.TabIndex = 7;
            this.lblTotalTitle.Text = "TOTAL AMOUNT";
            // 
            // lblTotalVal
            // 
            this.lblTotalVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalVal.Location = new System.Drawing.Point(20, 205);
            this.lblTotalVal.Name = "lblTotalVal";
            this.lblTotalVal.Size = new System.Drawing.Size(310, 45);
            this.lblTotalVal.TabIndex = 8;
            this.lblTotalVal.Text = "PHP 0.00";
            this.lblTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(242)))));
            this.btnCalculate.Location = new System.Drawing.Point(20, 275);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(310, 38);
            this.btnCalculate.TabIndex = 9;
            this.btnCalculate.Text = "🔄 Recalculate Totals";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(20, 325);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(310, 55);
            this.btnCheckout.TabIndex = 10;
            this.btnCheckout.Text = "💳 Confirm Checkout && Print PDF";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.BtnCheckout_Click);
            // 
            // StaffPOSDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlScan);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.pnlCartActions);
            this.Controls.Add(this.pnlTotals);
            this.Name = "StaffPOSDashboard";
            this.Size = new System.Drawing.Size(1080, 600);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlScan.ResumeLayout(false);
            this.pnlScan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartActions.ResumeLayout(false);
            this.pnlCartActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlScan;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnSampleBarcode;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label lblScanStatus;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlert;
        private System.Windows.Forms.Panel pnlCartActions;
        private System.Windows.Forms.Label lblQtyEdit;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Button btnUpdateQty;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblSubtotalTitle;
        private System.Windows.Forms.Label lblSubtotalVal;
        private System.Windows.Forms.Label lblDiscountTitle;
        private System.Windows.Forms.ComboBox cboDiscountPreset;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.Label lblTaxTitle;
        private System.Windows.Forms.Label lblTaxVal;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalVal;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnCheckout;
    }
}
