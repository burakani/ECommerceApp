namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Domain.Entities;

    /// <summary>
    /// Order Service Interface
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Add new order
        /// </summary>
        Task<string> Add(string userId);

        /// <summary>
        /// Complete an order
        /// </summary>
        Task CompleteOrder(string orderId);

        /// <summary>
        /// Cancel an order
        /// </summary>
        Task CancelOrder(string orderId);
    }
}
