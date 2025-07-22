namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Login request
    /// </summary>
    public class LoginRequest
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
