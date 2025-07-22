namespace ECommerceApp.Application.Services
{
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using ECommerceApp.Domain.Enums;

    using System;

    /// <summary>
    /// Order Service
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserBalanceRepository _userBalanceRepository;
        private readonly IBalanceManagementClient _balanceManagementClient;

        public OrderService(IProductService productService, IOrderRepository orderRepository, IUserBalanceRepository userBalanceRepository, IBalanceManagementClient balanceManagementClient)
        {
            _productService = productService;
            _orderRepository = orderRepository;
            _userBalanceRepository = userBalanceRepository;
            _balanceManagementClient = balanceManagementClient;
        }

        /// <summary>
        /// Add order
        /// </summary>
        public async Task<string> Add(string userId)
        {
            var products = await _productService.GetAvailableProductsAsync();

            if (!products.Any())
            {
                //log
                return string.Empty;
            }

            Random random = new Random();

            int index = random.Next(products.Count);

            var orderProduct = products[index];

            var isUserBalanceEnough = await CheckUserBalance(userId, orderProduct.Price);

            if (!isUserBalanceEnough)
            {
                //log
                return string.Empty;
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                Amount = orderProduct.Price,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                var preOrderRequest = new PreorderRequest
                {
                    OrderId = order.OrderId,
                    Amount = order.Amount
                };

                var preOrderResponse = await _balanceManagementClient.PreorderAsync(preOrderRequest);

                if (preOrderResponse.Success == false)
                {
                    //log
                    return string.Empty;
                }

                await _orderRepository.Add(order);

                await _userBalanceRepository.Lock(userId, orderProduct.Price * -1);

                await _orderRepository.Save();

                return order.OrderId;
            }
            catch (Exception ex)
            {
                throw new Exception("Order can not created. ", ex);
            }
        }

        /// <summary>
        /// Complete an order
        /// </summary>
        public async Task CompleteOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null || order.Status == OrderStatus.Completed)
            {
                throw new Exception("Order not found or already completed");
            }

            try
            {
                var completeOrder = new CompleteRequest
                {
                    OrderId = order.OrderId
                };

                var completeOrderResponse = await _balanceManagementClient.CompleteOrderAsync(completeOrder);

                if (completeOrderResponse.Success == false)
                {
                    //log
                    return;
                }

                await _orderRepository.CompleteOrder(orderId);

                await _userBalanceRepository.Update(order.UserId, (order.Amount * -1));

                await _orderRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Order can not completed. ", ex);
            }
        }

        /// <summary>
        /// Cancel order
        /// </summary>
        public async Task CancelOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null || order.Status == OrderStatus.Canceled)
            {
                throw new Exception("Order not found or already canceled");
            }

            try
            {
                await _orderRepository.CancelOrder(orderId);

                await _userBalanceRepository.Update(order.UserId, order.Amount);

                await _orderRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Order can not cancelled. ", ex);
            }
        }

        /// <summary>
        /// Check User Balance
        /// </summary>
        private async Task<bool> CheckUserBalance(string userId, decimal price)
        {
            var userAvailableBalance = await _userBalanceRepository.GetUserAvailableBalance(userId);

            if (userAvailableBalance < price)
            {
                return false;
            }

            return true;
        }
    }
}
