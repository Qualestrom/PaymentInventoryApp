namespace PaymentInventoryApp.Forms
{
    partial class ManagerInventoryDashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnRefreshAll = new System.Windows.Forms.Button();
            this.tabManager = new System.Windows.Forms.TabControl();
            this.tabBatches = new System.Windows.Forms.TabPage();
            this.dgvBatches = new System.Windows.Forms.DataGridView();
            this.tabAlerts = new System.Windows.Forms.TabPage();
            this.grpLowStock = new System.Windows.Forms.GroupBox();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.grpExpiring = new System.Windows.Forms.GroupBox();
            this.dgvExpiring = new System.Windows.Forms.DataGridView();
            this.tabIntake = new System.Windows.Forms.TabPage();
            this.pnlIntakeCard = new System.Windows.Forms.Panel();
            this.lblIntakeTitle = new System.Windows.Forms.Label();
            this.lblIntakeProd = new System.Windows.Forms.Label();
            this.cboIntakeProduct = new System.Windows.Forms.ComboBox();
            this.lblIntakeQty = new System.Windows.Forms.Label();
            this.numIntakeQty = new System.Windows.Forms.NumericUpDown();
            this.lblIntakeDelivery = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.lblIntakeExpiry = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnSubmitIntake = new System.Windows.Forms.Button();
            this.lblIntakeStatus = new System.Windows.Forms.Label();
            this.tabPrice = new System.Windows.Forms.TabPage();
            this.pnlPriceCard = new System.Windows.Forms.Panel();
            this.lblPriceTitle = new System.Windows.Forms.Label();
            this.lblPriceProd = new System.Windows.Forms.Label();
            this.cboPriceProduct = new System.Windows.Forms.ComboBox();
            this.lblCurrentPrice = new System.Windows.Forms.Label();
            this.lblCurrentPriceVal = new System.Windows.Forms.Label();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.numNewPrice = new System.Windows.Forms.NumericUpDown();
            this.btnUpdatePrice = new System.Windows.Forms.Button();
            this.lblPriceStatus = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.tabManager.SuspendLayout();
            this.tabBatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).BeginInit();
            this.tabAlerts.SuspendLayout();
            this.grpLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.grpExpiring.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiring)).BeginInit();
            this.tabIntake.SuspendLayout();
            this.pnlIntakeCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntakeQty)).BeginInit();
            this.tabPrice.SuspendLayout();
            this.pnlPriceCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNewPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Controls.Add(this.btnRefreshAll);
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
            this.lblHeader.Size = new System.Drawing.Size(378, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Manager Inventory Surveillance & Control";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 34);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(530, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Synchronized Data Tier: PostgreSQL Batches (store_inventory) • MySQL Master Catalog (store_sales)";
            // 
            // btnRefreshAll
            // 
            this.btnRefreshAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefreshAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefreshAll.ForeColor = System.Drawing.Color.White;
            this.btnRefreshAll.Location = new System.Drawing.Point(925, 14);
            this.btnRefreshAll.Name = "btnRefreshAll";
            this.btnRefreshAll.Size = new System.Drawing.Size(140, 32);
            this.btnRefreshAll.TabIndex = 2;
            this.btnRefreshAll.Text = "🔄 Refresh Data";
            this.btnRefreshAll.UseVisualStyleBackColor = false;
            this.btnRefreshAll.Click += new System.EventHandler(this.BtnRefreshAll_Click);
            // 
            // tabManager
            // 
            this.tabManager.Controls.Add(this.tabBatches);
            this.tabManager.Controls.Add(this.tabAlerts);
            this.tabManager.Controls.Add(this.tabIntake);
            this.tabManager.Controls.Add(this.tabPrice);
            this.tabManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabManager.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabManager.Location = new System.Drawing.Point(0, 60);
            this.tabManager.Name = "tabManager";
            this.tabManager.SelectedIndex = 0;
            this.tabManager.Size = new System.Drawing.Size(1080, 540);
            this.tabManager.TabIndex = 1;
            this.tabManager.SelectedIndexChanged += new System.EventHandler(this.TabManager_SelectedIndexChanged);
            // 
            // tabBatches
            // 
            this.tabBatches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabBatches.Controls.Add(this.dgvBatches);
            this.tabBatches.Location = new System.Drawing.Point(4, 25);
            this.tabBatches.Name = "tabBatches";
            this.tabBatches.Padding = new System.Windows.Forms.Padding(12);
            this.tabBatches.Size = new System.Drawing.Size(1072, 511);
            this.tabBatches.TabIndex = 0;
            this.tabBatches.Text = "📦 Inventory Batches (PostgreSQL)";
            // 
            // dgvBatches
            // 
            this.dgvBatches.AllowUserToAddRows = false;
            this.dgvBatches.AllowUserToDeleteRows = false;
            this.dgvBatches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBatches.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatches.Location = new System.Drawing.Point(12, 12);
            this.dgvBatches.MultiSelect = false;
            this.dgvBatches.Name = "dgvBatches";
            this.dgvBatches.ReadOnly = true;
            this.dgvBatches.RowHeadersVisible = false;
            this.dgvBatches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatches.Size = new System.Drawing.Size(1048, 487);
            this.dgvBatches.TabIndex = 0;
            // 
            // tabAlerts
            // 
            this.tabAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabAlerts.Controls.Add(this.grpLowStock);
            this.tabAlerts.Controls.Add(this.grpExpiring);
            this.tabAlerts.Location = new System.Drawing.Point(4, 25);
            this.tabAlerts.Name = "tabAlerts";
            this.tabAlerts.Padding = new System.Windows.Forms.Padding(12);
            this.tabAlerts.Size = new System.Drawing.Size(1072, 511);
            this.tabAlerts.TabIndex = 1;
            this.tabAlerts.Text = "🚨 Surveillance Alerts (Low-Stock & Expiry)";
            // 
            // grpLowStock
            // 
            this.grpLowStock.BackColor = System.Drawing.Color.White;
            this.grpLowStock.Controls.Add(this.dgvLowStock);
            this.grpLowStock.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.grpLowStock.Location = new System.Drawing.Point(12, 12);
            this.grpLowStock.Name = "grpLowStock";
            this.grpLowStock.Padding = new System.Windows.Forms.Padding(10);
            this.grpLowStock.Size = new System.Drawing.Size(515, 480);
            this.grpLowStock.TabIndex = 0;
            this.grpLowStock.TabStop = false;
            this.grpLowStock.Text = "⚠ Low Stock Threshold Warnings (< 10 units)";
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AllowUserToDeleteRows = false;
            this.dgvLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStock.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLowStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLowStock.Location = new System.Drawing.Point(10, 27);
            this.dgvLowStock.MultiSelect = false;
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.RowHeadersVisible = false;
            this.dgvLowStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLowStock.Size = new System.Drawing.Size(495, 443);
            this.dgvLowStock.TabIndex = 0;
            // 
            // grpExpiring
            // 
            this.grpExpiring.BackColor = System.Drawing.Color.White;
            this.grpExpiring.Controls.Add(this.dgvExpiring);
            this.grpExpiring.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpExpiring.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.grpExpiring.Location = new System.Drawing.Point(540, 12);
            this.grpExpiring.Name = "grpExpiring";
            this.grpExpiring.Padding = new System.Windows.Forms.Padding(10);
            this.grpExpiring.Size = new System.Drawing.Size(520, 480);
            this.grpExpiring.TabIndex = 1;
            this.grpExpiring.TabStop = false;
            this.grpExpiring.Text = "⏳ Imminent Expiration Warnings (Within 7 Days)";
            // 
            // dgvExpiring
            // 
            this.dgvExpiring.AllowUserToAddRows = false;
            this.dgvExpiring.AllowUserToDeleteRows = false;
            this.dgvExpiring.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpiring.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(249)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpiring.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvExpiring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpiring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpiring.Location = new System.Drawing.Point(10, 27);
            this.dgvExpiring.MultiSelect = false;
            this.dgvExpiring.Name = "dgvExpiring";
            this.dgvExpiring.ReadOnly = true;
            this.dgvExpiring.RowHeadersVisible = false;
            this.dgvExpiring.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpiring.Size = new System.Drawing.Size(500, 443);
            this.dgvExpiring.TabIndex = 0;
            // 
            // tabIntake
            // 
            this.tabIntake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabIntake.Controls.Add(this.pnlIntakeCard);
            this.tabIntake.Location = new System.Drawing.Point(4, 25);
            this.tabIntake.Name = "tabIntake";
            this.tabIntake.Padding = new System.Windows.Forms.Padding(12);
            this.tabIntake.Size = new System.Drawing.Size(1072, 511);
            this.tabIntake.TabIndex = 2;
            this.tabIntake.Text = "📥 Record Stock Arrival (Intake)";
            // 
            // pnlIntakeCard
            // 
            this.pnlIntakeCard.BackColor = System.Drawing.Color.White;
            this.pnlIntakeCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIntakeCard.Controls.Add(this.lblIntakeTitle);
            this.pnlIntakeCard.Controls.Add(this.lblIntakeProd);
            this.pnlIntakeCard.Controls.Add(this.cboIntakeProduct);
            this.pnlIntakeCard.Controls.Add(this.lblIntakeQty);
            this.pnlIntakeCard.Controls.Add(this.numIntakeQty);
            this.pnlIntakeCard.Controls.Add(this.lblIntakeDelivery);
            this.pnlIntakeCard.Controls.Add(this.dtpDeliveryDate);
            this.pnlIntakeCard.Controls.Add(this.lblIntakeExpiry);
            this.pnlIntakeCard.Controls.Add(this.dtpExpiryDate);
            this.pnlIntakeCard.Controls.Add(this.btnSubmitIntake);
            this.pnlIntakeCard.Controls.Add(this.lblIntakeStatus);
            this.pnlIntakeCard.Location = new System.Drawing.Point(260, 30);
            this.pnlIntakeCard.Name = "pnlIntakeCard";
            this.pnlIntakeCard.Size = new System.Drawing.Size(550, 440);
            this.pnlIntakeCard.TabIndex = 0;
            // 
            // lblIntakeTitle
            // 
            this.lblIntakeTitle.AutoSize = true;
            this.lblIntakeTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblIntakeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblIntakeTitle.Location = new System.Drawing.Point(30, 20);
            this.lblIntakeTitle.Name = "lblIntakeTitle";
            this.lblIntakeTitle.Size = new System.Drawing.Size(294, 25);
            this.lblIntakeTitle.TabIndex = 0;
            this.lblIntakeTitle.Text = "Supplier Stock Restocking Intake";
            // 
            // lblIntakeProd
            // 
            this.lblIntakeProd.AutoSize = true;
            this.lblIntakeProd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblIntakeProd.Location = new System.Drawing.Point(30, 70);
            this.lblIntakeProd.Name = "lblIntakeProd";
            this.lblIntakeProd.Size = new System.Drawing.Size(95, 17);
            this.lblIntakeProd.TabIndex = 1;
            this.lblIntakeProd.Text = "Select Product";
            // 
            // cboIntakeProduct
            // 
            this.cboIntakeProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntakeProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboIntakeProduct.FormattingEnabled = true;
            this.cboIntakeProduct.Location = new System.Drawing.Point(30, 92);
            this.cboIntakeProduct.Name = "cboIntakeProduct";
            this.cboIntakeProduct.Size = new System.Drawing.Size(480, 25);
            this.cboIntakeProduct.TabIndex = 2;
            // 
            // lblIntakeQty
            // 
            this.lblIntakeQty.AutoSize = true;
            this.lblIntakeQty.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblIntakeQty.Location = new System.Drawing.Point(30, 140);
            this.lblIntakeQty.Name = "lblIntakeQty";
            this.lblIntakeQty.Size = new System.Drawing.Size(126, 17);
            this.lblIntakeQty.TabIndex = 3;
            this.lblIntakeQty.Text = "Delivered Quantity";
            // 
            // numIntakeQty
            // 
            this.numIntakeQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numIntakeQty.Location = new System.Drawing.Point(30, 162);
            this.numIntakeQty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numIntakeQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIntakeQty.Name = "numIntakeQty";
            this.numIntakeQty.Size = new System.Drawing.Size(480, 25);
            this.numIntakeQty.TabIndex = 4;
            this.numIntakeQty.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblIntakeDelivery
            // 
            this.lblIntakeDelivery.AutoSize = true;
            this.lblIntakeDelivery.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblIntakeDelivery.Location = new System.Drawing.Point(30, 210);
            this.lblIntakeDelivery.Name = "lblIntakeDelivery";
            this.lblIntakeDelivery.Size = new System.Drawing.Size(92, 17);
            this.lblIntakeDelivery.TabIndex = 5;
            this.lblIntakeDelivery.Text = "Delivery Date";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(30, 232);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(480, 25);
            this.dtpDeliveryDate.TabIndex = 6;
            // 
            // lblIntakeExpiry
            // 
            this.lblIntakeExpiry.AutoSize = true;
            this.lblIntakeExpiry.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblIntakeExpiry.Location = new System.Drawing.Point(30, 280);
            this.lblIntakeExpiry.Name = "lblIntakeExpiry";
            this.lblIntakeExpiry.Size = new System.Drawing.Size(79, 17);
            this.lblIntakeExpiry.TabIndex = 7;
            this.lblIntakeExpiry.Text = "Expiry Date";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiryDate.Location = new System.Drawing.Point(30, 302);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(480, 25);
            this.dtpExpiryDate.TabIndex = 8;
            // 
            // btnSubmitIntake
            // 
            this.btnSubmitIntake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSubmitIntake.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmitIntake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitIntake.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSubmitIntake.ForeColor = System.Drawing.Color.White;
            this.btnSubmitIntake.Location = new System.Drawing.Point(30, 350);
            this.btnSubmitIntake.Name = "btnSubmitIntake";
            this.btnSubmitIntake.Size = new System.Drawing.Size(480, 42);
            this.btnSubmitIntake.TabIndex = 9;
            this.btnSubmitIntake.Text = "✔ Log Stock Arrival into PostgreSQL";
            this.btnSubmitIntake.UseVisualStyleBackColor = false;
            this.btnSubmitIntake.Click += new System.EventHandler(this.BtnSubmitIntake_Click);
            // 
            // lblIntakeStatus
            // 
            this.lblIntakeStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIntakeStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblIntakeStatus.Location = new System.Drawing.Point(30, 400);
            this.lblIntakeStatus.Name = "lblIntakeStatus";
            this.lblIntakeStatus.Size = new System.Drawing.Size(480, 23);
            this.lblIntakeStatus.TabIndex = 10;
            this.lblIntakeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPrice
            // 
            this.tabPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPrice.Controls.Add(this.pnlPriceCard);
            this.tabPrice.Location = new System.Drawing.Point(4, 25);
            this.tabPrice.Name = "tabPrice";
            this.tabPrice.Padding = new System.Windows.Forms.Padding(12);
            this.tabPrice.Size = new System.Drawing.Size(1072, 511);
            this.tabPrice.TabIndex = 3;
            this.tabPrice.Text = "💲 Product Price Management (MySQL)";
            // 
            // pnlPriceCard
            // 
            this.pnlPriceCard.BackColor = System.Drawing.Color.White;
            this.pnlPriceCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPriceCard.Controls.Add(this.lblPriceTitle);
            this.pnlPriceCard.Controls.Add(this.lblPriceProd);
            this.pnlPriceCard.Controls.Add(this.cboPriceProduct);
            this.pnlPriceCard.Controls.Add(this.lblCurrentPrice);
            this.pnlPriceCard.Controls.Add(this.lblCurrentPriceVal);
            this.pnlPriceCard.Controls.Add(this.lblNewPrice);
            this.pnlPriceCard.Controls.Add(this.numNewPrice);
            this.pnlPriceCard.Controls.Add(this.btnUpdatePrice);
            this.pnlPriceCard.Controls.Add(this.lblPriceStatus);
            this.pnlPriceCard.Location = new System.Drawing.Point(260, 40);
            this.pnlPriceCard.Name = "pnlPriceCard";
            this.pnlPriceCard.Size = new System.Drawing.Size(550, 380);
            this.pnlPriceCard.TabIndex = 0;
            // 
            // lblPriceTitle
            // 
            this.lblPriceTitle.AutoSize = true;
            this.lblPriceTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPriceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblPriceTitle.Location = new System.Drawing.Point(30, 20);
            this.lblPriceTitle.Name = "lblPriceTitle";
            this.lblPriceTitle.Size = new System.Drawing.Size(326, 25);
            this.lblPriceTitle.TabIndex = 0;
            this.lblPriceTitle.Text = "Manager Product Price Management";
            // 
            // lblPriceProd
            // 
            this.lblPriceProd.AutoSize = true;
            this.lblPriceProd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPriceProd.Location = new System.Drawing.Point(30, 70);
            this.lblPriceProd.Name = "lblPriceProd";
            this.lblPriceProd.Size = new System.Drawing.Size(95, 17);
            this.lblPriceProd.TabIndex = 1;
            this.lblPriceProd.Text = "Select Product";
            // 
            // cboPriceProduct
            // 
            this.cboPriceProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriceProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPriceProduct.FormattingEnabled = true;
            this.cboPriceProduct.Location = new System.Drawing.Point(30, 92);
            this.cboPriceProduct.Name = "cboPriceProduct";
            this.cboPriceProduct.Size = new System.Drawing.Size(480, 25);
            this.cboPriceProduct.TabIndex = 2;
            this.cboPriceProduct.SelectedIndexChanged += new System.EventHandler(this.CboPriceProduct_SelectedIndexChanged);
            // 
            // lblCurrentPrice
            // 
            this.lblCurrentPrice.AutoSize = true;
            this.lblCurrentPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCurrentPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblCurrentPrice.Location = new System.Drawing.Point(30, 140);
            this.lblCurrentPrice.Name = "lblCurrentPrice";
            this.lblCurrentPrice.Size = new System.Drawing.Size(126, 17);
            this.lblCurrentPrice.TabIndex = 3;
            this.lblCurrentPrice.Text = "Current Retail Price:";
            // 
            // lblCurrentPriceVal
            // 
            this.lblCurrentPriceVal.AutoSize = true;
            this.lblCurrentPriceVal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPriceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblCurrentPriceVal.Location = new System.Drawing.Point(165, 137);
            this.lblCurrentPriceVal.Name = "lblCurrentPriceVal";
            this.lblCurrentPriceVal.Size = new System.Drawing.Size(78, 21);
            this.lblCurrentPriceVal.TabIndex = 4;
            this.lblCurrentPriceVal.Text = "PHP 0.00";
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.AutoSize = true;
            this.lblNewPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNewPrice.Location = new System.Drawing.Point(30, 185);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(139, 17);
            this.lblNewPrice.TabIndex = 5;
            this.lblNewPrice.Text = "New Unit Price (PHP)";
            // 
            // numNewPrice
            // 
            this.numNewPrice.DecimalPlaces = 2;
            this.numNewPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numNewPrice.Location = new System.Drawing.Point(30, 207);
            this.numNewPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numNewPrice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numNewPrice.Name = "numNewPrice";
            this.numNewPrice.Size = new System.Drawing.Size(480, 25);
            this.numNewPrice.TabIndex = 6;
            this.numNewPrice.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnUpdatePrice
            // 
            this.btnUpdatePrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnUpdatePrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdatePrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdatePrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdatePrice.ForeColor = System.Drawing.Color.White;
            this.btnUpdatePrice.Location = new System.Drawing.Point(30, 260);
            this.btnUpdatePrice.Name = "btnUpdatePrice";
            this.btnUpdatePrice.Size = new System.Drawing.Size(480, 42);
            this.btnUpdatePrice.TabIndex = 7;
            this.btnUpdatePrice.Text = "💾 Save New Price to MySQL";
            this.btnUpdatePrice.UseVisualStyleBackColor = false;
            this.btnUpdatePrice.Click += new System.EventHandler(this.BtnUpdatePrice_Click);
            // 
            // lblPriceStatus
            // 
            this.lblPriceStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPriceStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblPriceStatus.Location = new System.Drawing.Point(30, 320);
            this.lblPriceStatus.Name = "lblPriceStatus";
            this.lblPriceStatus.Size = new System.Drawing.Size(480, 23);
            this.lblPriceStatus.TabIndex = 8;
            this.lblPriceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerInventoryDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.tabManager);
            this.Controls.Add(this.pnlTop);
            this.Name = "ManagerInventoryDashboard";
            this.Size = new System.Drawing.Size(1080, 600);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabManager.ResumeLayout(false);
            this.tabBatches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).EndInit();
            this.tabAlerts.ResumeLayout(false);
            this.grpLowStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.grpExpiring.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiring)).EndInit();
            this.tabIntake.ResumeLayout(false);
            this.pnlIntakeCard.ResumeLayout(false);
            this.pnlIntakeCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntakeQty)).EndInit();
            this.tabPrice.ResumeLayout(false);
            this.pnlPriceCard.ResumeLayout(false);
            this.pnlPriceCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNewPrice)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnRefreshAll;
        private System.Windows.Forms.TabControl tabManager;
        private System.Windows.Forms.TabPage tabBatches;
        private System.Windows.Forms.DataGridView dgvBatches;
        private System.Windows.Forms.TabPage tabAlerts;
        private System.Windows.Forms.GroupBox grpLowStock;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.GroupBox grpExpiring;
        private System.Windows.Forms.DataGridView dgvExpiring;
        private System.Windows.Forms.TabPage tabIntake;
        private System.Windows.Forms.Panel pnlIntakeCard;
        private System.Windows.Forms.Label lblIntakeTitle;
        private System.Windows.Forms.Label lblIntakeProd;
        private System.Windows.Forms.ComboBox cboIntakeProduct;
        private System.Windows.Forms.Label lblIntakeQty;
        private System.Windows.Forms.NumericUpDown numIntakeQty;
        private System.Windows.Forms.Label lblIntakeDelivery;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label lblIntakeExpiry;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnSubmitIntake;
        private System.Windows.Forms.Label lblIntakeStatus;
        private System.Windows.Forms.TabPage tabPrice;
        private System.Windows.Forms.Panel pnlPriceCard;
        private System.Windows.Forms.Label lblPriceTitle;
        private System.Windows.Forms.Label lblPriceProd;
        private System.Windows.Forms.ComboBox cboPriceProduct;
        private System.Windows.Forms.Label lblCurrentPrice;
        private System.Windows.Forms.Label lblCurrentPriceVal;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.NumericUpDown numNewPrice;
        private System.Windows.Forms.Button btnUpdatePrice;
        private System.Windows.Forms.Label lblPriceStatus;
    }
}
