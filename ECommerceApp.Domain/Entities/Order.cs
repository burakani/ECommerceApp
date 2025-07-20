namespace ECommerceApp.Domain.Entities
{
    using ECommerceApp.Domain.Enums;

    /// <summary>
    /// Order Entity
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Order Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Order Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}