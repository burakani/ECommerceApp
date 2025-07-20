namespace ECommerceApp.Application.DTOs
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Pre-order Response DTO
    /// </summary>
    public class PreorderResponse
    {
        /// <summary>
        /// Is Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Response Message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
