namespace ECommerceApp.Infrastructure.Clients
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Application.DTOs;

    /// <summary>
    /// Balance Management Client
    /// </summary>
    public class BalanceManagementClient : IBalanceManagementClient
    {
        private readonly HttpClient _httpClient;

        private const int MaxRetry = 3;

        public BalanceManagementClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get Products
        /// </summary>
        public async Task<List<ProductDto>> GetProductsAsync(int retryCount = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/products");
                
                if(response.IsSuccessStatusCode == false)
                {
                    // Log the failure
                    return new List<ProductDto>();
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ProductDto>>>(content);

                if (apiResponse?.Success == false || apiResponse?.Data == null)
                {
                    // Log the failure
                    return new List<ProductDto>();
                }

                return apiResponse?.Data!;
            }
            catch (Exception ex)
            {
                if (retryCount >= MaxRetry)
                {
                    // Log the final failure
                    throw new Exception(ex.Message);                    
                }

                await Task.Delay(100); // Wait before retrying

                return await GetProductsAsync(retryCount + 1);
            }
        }

        /// <summary>
        /// Pre order
        /// </summary>
        public async Task<PreorderResponse> PreorderAsync(PreorderRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/preorder", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PreorderResponse>(result)!;
        }

        /// <summary>
        /// Complete Order
        /// </summary>
        public async Task<CompleteResponse> CompleteOrderAsync(CompleteRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/complete", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CompleteResponse>(result)!;
        }
    }
}
