namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Authentication response
    /// </summary>
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public string? Token { get; set; }
    }
}
