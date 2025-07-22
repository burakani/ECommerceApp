namespace ECommerceApp.Application.DTOs
{
    /// <summary>
    /// Jwt Settings
    /// </summary>
    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpireMinutes { get; set; }
    }
}
