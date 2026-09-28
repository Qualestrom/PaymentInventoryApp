using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using PaymentInventoryApp.Models;

namespace PaymentInventoryApp.Services
{
    /// <summary>
    /// Receipt generation service using System.Drawing.Printing.
    /// Simulates a POS thermal receipt printer by routing output to the Windows 'Microsoft Print to PDF' driver.
    /// </summary>
    public class PdfReceiptService
    {
        private SaleTransaction? _currentTransaction;
        private readonly Font _titleFont = new("Courier New", 12, FontStyle.Bold);
        private readonly Font _boldFont = new("Courier New", 9, FontStyle.Bold);
        private readonly Font _regularFont = new("Courier New", 8, FontStyle.Regular);
        private readonly Font _smallFont = new("Courier New", 7, FontStyle.Regular);

        /// <summary>
        /// Prints the receipt for the specified transaction.
        /// Targets Microsoft Print to PDF to produce a receipt PDF file on disk.
        /// </summary>
        public bool PrintReceipt(SaleTransaction transaction, IWin32Window? owner = null)
        {
            _currentTransaction = transaction;

            try
            {
                using var printDoc = new PrintDocument();
                printDoc.DocumentName = $"Receipt_TX_{transaction.TransactionId}_{DateTime.Now:yyyyMMdd_HHmmss}";

                // Configure paper size for standard 80mm thermal receipt
                // 80mm = ~3.15 inches = ~315 hundredths of an inch
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Thermal80mm", 315, 600);
                printDoc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

                // Find Microsoft Print to PDF or prompt printer selection
                bool foundPdfPrinter = false;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer.Contains("PDF", StringComparison.OrdinalIgnoreCase))
                    {
                        printDoc.PrinterSettings.PrinterName = printer;
                        foundPdfPrinter = true;
                        break;
                    }
                }

                printDoc.PrintPage += OnPrintPage;

                // If Microsoft Print to PDF is selected, specify the output file name via SaveFileDialog or desktop
                if (foundPdfPrinter)
                {
                    using var saveDialog = new SaveFileDialog
                    {
                        Filter = "PDF Files (*.pdf)|*.pdf",
                        Title = "Save Customer Receipt PDF (Thermal Simulation)",
                        FileName = $"Receipt_{transaction.TransactionId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    };

                    if (saveDialog.ShowDialog(owner) == DialogResult.OK)
                    {
                        printDoc.PrinterSettings.PrintToFile = true;
                        printDoc.PrinterSettings.PrintFileName = saveDialog.FileName;
                        printDoc.Print();
                        return true;
                    }
                    else
                    {
                        // User canceled save
                        return false;
                    }
                }
                else
                {
                    // Fallback to standard PrintDialog if PDF printer not pre-selected
                    using var printDialog = new PrintDialog { Document = printDoc };
                    if (printDialog.ShowDialog(owner) == DialogResult.OK)
                    {
                        printDoc.Print();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Receipt Printing Error: {ex.Message}", "Printer Simulation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentTransaction == null || e.Graphics == null) return;

            var g = e.Graphics;
            float y = 15;
            float leftMargin = 15;
            float printableWidth = 280;
            float rightMargin = leftMargin + printableWidth;

            var centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            var rightFormat = new StringFormat { Alignment = StringAlignment.Far };
            var leftFormat = new StringFormat { Alignment = StringAlignment.Near };

            // Store Header
            g.DrawString("ACME RETAIL STORE", _titleFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 20), centerFormat);
            y += 20;
            g.DrawString("123 Engineering Avenue, Campus", _smallFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);
            y += 14;
            g.DrawString("VAT Reg. TIN: 000-123-456-789", _smallFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);
            y += 14;
            g.DrawString("POS Terminal #01 (Simulation)", _smallFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);
            y += 18;

            // Receipt Metadata
            g.DrawString($"Receipt No: TX-{_currentTransaction.TransactionId:D6}", _boldFont, Brushes.Black, leftMargin, y);
            y += 15;
            g.DrawString($"Date/Time : {_currentTransaction.Date:yyyy-MM-dd HH:mm:ss}", _regularFont, Brushes.Black, leftMargin, y);
            y += 14;
            g.DrawString($"Cashier   : {_currentTransaction.CashierName}", _regularFont, Brushes.Black, leftMargin, y);
            y += 16;

            // Separator
            g.DrawString(new string('-', 38), _regularFont, Brushes.Black, leftMargin, y);
            y += 14;

            // Item Column Headers
            g.DrawString("ITEM / QTY", _boldFont, Brushes.Black, leftMargin, y);
            g.DrawString("PRICE", _boldFont, Brushes.Black, leftMargin + 140, y);
            g.DrawString("TOTAL", _boldFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), rightFormat);
            y += 16;
            g.DrawString(new string('-', 38), _regularFont, Brushes.Black, leftMargin, y);
            y += 14;

            // Line Items
            foreach (var item in _currentTransaction.Items)
            {
                // Item Name
                g.DrawString(item.ProductName, _boldFont, Brushes.Black, leftMargin, y);
                y += 14;

                // Qty x Unit Price -> Line Total
                string qtyPrice = $"{item.Quantity} @ PHP {item.UnitPrice:F2}";
                g.DrawString(qtyPrice, _regularFont, Brushes.Black, leftMargin + 10, y);
                g.DrawString($"PHP {item.LineTotal:F2}", _boldFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), rightFormat);
                y += 16;
            }

            // Separator
            g.DrawString(new string('-', 38), _regularFont, Brushes.Black, leftMargin, y);
            y += 14;

            // Subtotal
            g.DrawString("Subtotal:", _regularFont, Brushes.Black, leftMargin, y);
            g.DrawString($"PHP {_currentTransaction.Subtotal:F2}", _regularFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), rightFormat);
            y += 15;

            // Discount
            if (_currentTransaction.DiscountPercent > 0)
            {
                decimal discountAmt = _currentTransaction.Subtotal * (_currentTransaction.DiscountPercent / 100m);
                g.DrawString($"Discount ({_currentTransaction.DiscountPercent:F0}%):", _regularFont, Brushes.Black, leftMargin, y);
                g.DrawString($"-PHP {discountAmt:F2}", _regularFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), rightFormat);
                y += 15;
            }

            // 12% VAT
            g.DrawString("VAT (12%):", _regularFont, Brushes.Black, leftMargin, y);
            g.DrawString($"PHP {_currentTransaction.TaxAmount:F2}", _regularFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), rightFormat);
            y += 18;

            // Net Total
            g.DrawString(new string('=', 38), _boldFont, Brushes.Black, leftMargin, y);
            y += 14;
            g.DrawString("TOTAL AMOUNT:", _titleFont, Brushes.Black, leftMargin, y);
            g.DrawString($"PHP {_currentTransaction.TotalAmount:F2}", _titleFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 20), rightFormat);
            y += 24;
            g.DrawString(new string('=', 38), _boldFont, Brushes.Black, leftMargin, y);
            y += 18;

            // Footer
            g.DrawString("THANK YOU FOR YOUR PURCHASE!", _boldFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);
            y += 14;
            g.DrawString("Please retain this receipt for warranty.", _smallFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);
            y += 14;
            g.DrawString($"* * * TX-{_currentTransaction.TransactionId:D6} * * *", _smallFont, Brushes.Black, new RectangleF(leftMargin, y, printableWidth, 15), centerFormat);

            e.HasMorePages = false;
        }
    }
}
