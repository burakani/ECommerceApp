namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Application.DTOs;

    /// <summary>
    /// Product Service Interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get Available Products
        /// </summary>
        Task<List<ProductDto>> GetAvailableProductsAsync();
    }
}
