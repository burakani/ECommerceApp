namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Application.DTOs;

    /// <summary>
    /// User Service Interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Register
        /// </summary>
        Task<AuthResponse> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Login
        /// </summary>
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
