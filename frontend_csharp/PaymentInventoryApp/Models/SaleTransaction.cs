namespace PaymentInventoryApp.Models
{
    /// <summary>
    /// Represents a completed sales transaction stored in MySQL (store_sales).
    /// </summary>
    public class SaleTransaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CashierId { get; set; }
        public string CashierName { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new();
    }
}
