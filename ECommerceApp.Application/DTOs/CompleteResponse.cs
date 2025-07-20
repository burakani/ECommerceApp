namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Complete Response DTO
    /// </summary>
    public class CompleteResponse
    {
        /// <summary>
        /// Is Successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response Message
        /// </summary>
        public string? Message { get; set; }
    }
}
