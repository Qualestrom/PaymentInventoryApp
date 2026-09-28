using System;
using System.Drawing;
using System.Windows.Forms;
using PaymentInventoryApp.Models;
using PaymentInventoryApp.Services;

namespace PaymentInventoryApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly User _currentUser;
        private readonly PythonBridge _bridge;
        private StaffPOSDashboard? _posDashboard;
        private ManagerInventoryDashboard? _mgrDashboard;

        public MainForm(User user, PythonBridge bridge)
        {
            InitializeComponent();
            _currentUser = user;
            _bridge = bridge;

            SetupUserSession();
            InitializeDashboards();
        }

        private void SetupUserSession()
        {
            lblUserInfo.Text = $"User: {_currentUser.FullName}";

            // Role-Based Access Control (RBAC) Enforcement
            if (_currentUser.IsManager)
            {
                lblRoleBadge.Text = "MANAGER";
                lblRoleBadge.BackColor = Color.FromArgb(46, 204, 113); // Green badge
            }
            else
            {
                lblRoleBadge.Text = "CASHIER";
                lblRoleBadge.BackColor = Color.FromArgb(52, 152, 219); // Blue badge

                // Cashier role restriction: Lock/Remove Manager Inventory Surveillance tab
                tabMain.TabPages.Remove(tabInventory);
            }
        }

        private void InitializeDashboards()
        {
            // 1. Initialize Staff POS Dashboard
            _posDashboard = new StaffPOSDashboard
            {
                Dock = DockStyle.Fill
            };
            _posDashboard.Initialize(_currentUser, _bridge);
            tabPOS.Controls.Add(_posDashboard);

            // 2. Initialize Manager Inventory Dashboard (if clearance permits)
            if (_currentUser.IsManager)
            {
                _mgrDashboard = new ManagerInventoryDashboard
                {
                    Dock = DockStyle.Fill
                };
                _mgrDashboard.Initialize(_currentUser, _bridge);
                tabInventory.Controls.Add(_mgrDashboard);
            }
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to sign out?", "Confirm Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Tag = "Logout";
                this.Close();
            }
        }
    }
}
