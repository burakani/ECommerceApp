namespace ECommerceApp.API.Extensions
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Application.Services;
    using ECommerceApp.Infrastructure.Clients;
    using ECommerceApp.Infrastructure.Persistence;
    using ECommerceApp.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// API Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add Db Context
        /// </summary>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<AppDbContext>();

            return services;
        }

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
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserBalanceRepository, UserBalanceRepository>();

            return services;
        }
    }
}
