using System;
using System.Drawing;
using System.Windows.Forms;
using PaymentInventoryApp.Models;
using PaymentInventoryApp.Services;

namespace PaymentInventoryApp.Forms
{
    public partial class LoginForm : Form
    {
        private readonly PythonBridge _bridge;

        public LoginForm()
        {
            InitializeComponent();
            _bridge = new PythonBridge();
            txtUsername.Text = "admin"; // Default convenient seed for testing
            txtPassword.Text = "admin123";
        }

        public LoginForm(PythonBridge bridge)
        {
            InitializeComponent();
            _bridge = bridge;
            txtUsername.Text = "admin";
            txtPassword.Text = "admin123";
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblStatus.Text = "Please enter both username and password.";
                return;
            }

            lblStatus.ForeColor = Color.FromArgb(0, 120, 212);
            lblStatus.Text = "Authenticating with database bridge...";
            btnLogin.Enabled = false;
            Application.DoEvents();

            var (success, user, error) = _bridge.AuthenticateUser(username, password);

            btnLogin.Enabled = true;

            if (success && user != null)
            {
                lblStatus.Text = "";
                // Launch MainForm with authenticated user context
                var mainForm = new MainForm(user, _bridge);
                this.Hide();
                mainForm.FormClosed += (s, args) =>
                {
                    // If not explicitly logging out to re-login, exit app
                    if (mainForm.Tag as string == "Logout")
                    {
                        txtPassword.Text = "";
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                };
                mainForm.Show();
            }
            else
            {
                lblStatus.ForeColor = Color.FromArgb(216, 44, 13);
                lblStatus.Text = error ?? "Invalid username or password.";
            }
        }
    }
}
