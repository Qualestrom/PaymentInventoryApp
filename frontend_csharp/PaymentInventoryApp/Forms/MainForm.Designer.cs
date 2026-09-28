namespace PaymentInventoryApp.Forms
{
    partial class MainForm
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
            this.pnlNavbar = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblRoleBadge = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPOS = new System.Windows.Forms.TabPage();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlNavbar.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavbar
            // 
            this.pnlNavbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.pnlNavbar.Controls.Add(this.lblAppTitle);
            this.pnlNavbar.Controls.Add(this.lblUserInfo);
            this.pnlNavbar.Controls.Add(this.lblRoleBadge);
            this.pnlNavbar.Controls.Add(this.btnLogout);
            this.pnlNavbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavbar.Location = new System.Drawing.Point(0, 0);
            this.pnlNavbar.Name = "pnlNavbar";
            this.pnlNavbar.Size = new System.Drawing.Size(1120, 52);
            this.pnlNavbar.TabIndex = 0;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(15, 14);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(425, 21);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "Automated Payment && Inventory Management System";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this.lblUserInfo.Location = new System.Drawing.Point(620, 15);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(260, 22);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Logged in: Administrator";
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRoleBadge
            // 
            this.lblRoleBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoleBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRoleBadge.ForeColor = System.Drawing.Color.White;
            this.lblRoleBadge.Location = new System.Drawing.Point(890, 15);
            this.lblRoleBadge.Name = "lblRoleBadge";
            this.lblRoleBadge.Size = new System.Drawing.Size(95, 22);
            this.lblRoleBadge.TabIndex = 2;
            this.lblRoleBadge.Text = "MANAGER";
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1005, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(95, 28);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Sign Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPOS);
            this.tabMain.Controls.Add(this.tabInventory);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabMain.ItemSize = new System.Drawing.Size(220, 32);
            this.tabMain.Location = new System.Drawing.Point(0, 52);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1120, 646);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 1;
            // 
            // tabPOS
            // 
            this.tabPOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPOS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPOS.Location = new System.Drawing.Point(4, 36);
            this.tabPOS.Name = "tabPOS";
            this.tabPOS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPOS.Size = new System.Drawing.Size(1112, 606);
            this.tabPOS.TabIndex = 0;
            this.tabPOS.Text = "🛒 Staff POS Checkout";
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabInventory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabInventory.Location = new System.Drawing.Point(4, 36);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(1112, 606);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "🏢 Manager Inventory Control";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMsg});
            this.statusStrip.Location = new System.Drawing.Point(0, 698);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1120, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(100)))));
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(430, 17);
            this.lblStatusMsg.Text = "Connected to Python Application Logic Tier • MySQL (store_sales) • PostgreSQL (store_inventory)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pnlNavbar);
            this.MinimumSize = new System.Drawing.Size(1100, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automated Payment and Inventory Management Desktop Application";
            this.pnlNavbar.ResumeLayout(false);
            this.pnlNavbar.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlNavbar;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblRoleBadge;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPOS;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMsg;
    }
}
