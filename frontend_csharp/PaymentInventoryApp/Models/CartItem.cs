namespace PaymentInventoryApp.Models
{
    /// <summary>
    /// Represents an active item in the Staff POS cart.
    /// Includes Smart Alert indicators for items nearing batch expiration.
    /// </summary>
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal LineTotal => UnitPrice * Quantity;

        // Smart Alert flags
        public bool IsNearExpiry { get; set; }
        public int? DaysUntilExpiry { get; set; }
        public string ExpiryAlertText => IsNearExpiry 
            ? $"⚠ Near Expiry ({DaysUntilExpiry}d remaining)" 
            : "OK";
    }
}
