namespace ECommerceApp.API.Extensions
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Application.Services;
    using ECommerceApp.Infrastructure.Clients;

    /// <summary>
    /// API Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add External Clients
        /// </summary>
        public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["BalanceManagement:BaseUrl"];

            services.AddHttpClient<IBalanceManagementClient, BalanceManagementClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl!);
            });

            return services;
        }

        /// <summary>
        /// Add Application Services
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
