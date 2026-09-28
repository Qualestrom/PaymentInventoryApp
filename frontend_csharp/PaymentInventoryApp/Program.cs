using System;
using System.Windows.Forms;
using PaymentInventoryApp.Forms;

namespace PaymentInventoryApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Launches the initial authentication window (LoginForm).
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}