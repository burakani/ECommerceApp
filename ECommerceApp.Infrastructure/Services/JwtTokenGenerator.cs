namespace ECommerceApp.Infrastructure.Services
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using Microsoft.Extensions.Options;
    using System.Security.Claims;
    using System.Text;
    using System.IdentityModel.Tokens.Jwt;
    using ECommerceApp.Application.DTOs;
    using Microsoft.IdentityModel.Tokens;

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwt;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwt = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var token = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
