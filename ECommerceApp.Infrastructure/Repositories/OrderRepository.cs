namespace ECommerceApp.Infrastructure.Repositories
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using ECommerceApp.Domain.Enums;
    using ECommerceApp.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Order Repository
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Order by ID
        /// </summary>
        public async Task<Order?> GetOrderById(string orderId)
        {
            return await _context.Orders.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Add
        /// </summary>
        public async Task Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            if (string.IsNullOrEmpty(order.OrderId))
            {
                order.OrderId = Guid.NewGuid().ToString();
            }

            _context.Orders.Add(order);
        }

        /// <summary>
        /// Cancel
        /// </summary>
        public async Task CancelOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            var order = await _context.Orders.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            order!.Status = OrderStatus.Canceled;

            _context.Orders.Update(order);
        }

        /// <summary>
        /// Complete
        /// </summary>
        public async Task CompleteOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            var order = await _context.Orders.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            order!.Status = OrderStatus.Completed;

            _context.Orders.Update(order);
        }

        /// <summary>
        /// Save
        /// </summary>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
