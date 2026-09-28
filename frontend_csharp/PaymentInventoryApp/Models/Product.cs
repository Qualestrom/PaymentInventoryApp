using System.Text.Json.Serialization;

namespace PaymentInventoryApp.Models
{
    /// <summary>
    /// Master product entity stored in MySQL (store_sales).
    /// </summary>
    public class Product
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("is_near_expiry")]
        public bool IsNearExpiry { get; set; }

        [JsonPropertyName("days_until_expiry")]
        public int? DaysUntilExpiry { get; set; }

        [JsonPropertyName("near_expiry_batch_id")]
        public int? NearExpiryBatchId { get; set; }
    }
}
