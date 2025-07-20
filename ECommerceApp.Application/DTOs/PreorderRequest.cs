namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Pre-order Request DTO
    /// </summary>
    public class PreorderRequest
    {
        /// <summary>
        /// Order ID
        /// </summary>
        public string OrderId { get; set; } = default!;

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}
