namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Pre-order Response DTO
    /// </summary>
    public class PreorderResponse
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
