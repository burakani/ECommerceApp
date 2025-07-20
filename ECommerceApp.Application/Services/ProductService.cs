using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Interfaces;

namespace ECommerceApp.Application.Services
{
    /// <summary>
    /// Product Service
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IBalanceManagementClient _balanceClient;

        public ProductService(IBalanceManagementClient balanceClient)
        {
            _balanceClient = balanceClient;
        }

        /// <summary>
        /// Get Available Products
        /// </summary>
        public async Task<List<ProductDto>> GetAvailableProductsAsync()
        {
            return await _balanceClient.GetProductsAsync();
        }
    }
}
