namespace ECommerceApp.Application.DTOs
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Complete Response DTO
    /// </summary>
    public class CompleteResponse
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
