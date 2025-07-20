namespace ECommerceApp.Application.DTOs
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Balance Management API Response
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Is Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
