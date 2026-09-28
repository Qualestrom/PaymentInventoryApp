using System.Text.Json.Serialization;

namespace PaymentInventoryApp.Models
{
    /// <summary>
    /// Represents an individual stock batch delivered from suppliers.
    /// Stored in PostgreSQL (store_inventory).
    /// </summary>
    public class InventoryBatch
    {
        [JsonPropertyName("batch_id")]
        public int BatchId { get; set; }

        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("delivery_date")]
        public string DeliveryDate { get; set; } = string.Empty;

        [JsonPropertyName("expiry_date")]
        public string ExpiryDate { get; set; } = string.Empty;

        [JsonPropertyName("days_remaining")]
        public int? DaysRemaining { get; set; }
    }
}
