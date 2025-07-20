namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Application.DTOs;

    /// <summary>
    /// Balance Management Client Interface
    /// </summary>
    public interface IBalanceManagementClient
    {
        /// <summary>
        /// Get Products
        /// </summary>
        Task<List<ProductDto>> GetProductsAsync(int retryCount = 0);

        /// <summary>
        /// Pre-order
        /// </summary>
        Task<PreorderResponse> PreorderAsync(PreorderRequest request);

        /// <summary>
        /// Complete Order
        /// </summary>
        Task<CompleteResponse> CompleteOrderAsync(CompleteRequest request);
    }
}
