namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Register request
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = default!;

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
