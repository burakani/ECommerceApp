namespace ECommerceApp.Application.DTOs
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Product DTO
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Product ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Product Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Product Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;

        /// <summary>
        /// Product Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Product Currency
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;

        /// <summary>
        /// Product Category
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = default!;

        /// <summary>
        /// Product Stock
        /// </summary>
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}
