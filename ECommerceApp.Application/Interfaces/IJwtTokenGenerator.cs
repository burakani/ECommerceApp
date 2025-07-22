namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Domain.Entities;

    /// <summary>
    /// Authentication Token Generator Interface
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generate JWT Token for a user
        /// </summary>
        string GenerateToken(User user);
    }
}
