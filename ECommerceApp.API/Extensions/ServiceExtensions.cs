namespace ECommerceApp.API.Extensions
{
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Application.Services;
    using ECommerceApp.Infrastructure.Clients;
    using ECommerceApp.Infrastructure.Persistence;
    using ECommerceApp.Infrastructure.Repositories;
    using ECommerceApp.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    /// <summary>
    /// API Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add JWT Authentication
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
