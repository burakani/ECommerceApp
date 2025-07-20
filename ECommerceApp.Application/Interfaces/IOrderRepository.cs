namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Domain.Entities;

    /// <summary>
    /// Order Repository Interface
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Get Order by ID
        /// </summary>
        Task<Order?> GetOrderById(string orderId);

        /// <summary>
        /// Add new order
        /// </summary>
        Task Add(Order order);

        /// <summary>
        /// Complete an order
        /// </summary>
        Task CompleteOrder(string orderId);

        /// <summary>
        /// Cancel an order
        /// </summary>
        Task CancelOrder(string orderId);

        /// <summary>
        /// Save
        /// </summary>
        Task Save();
    }
}
