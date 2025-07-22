namespace ECommerceApp.Application.Services
{
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;

    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserBalanceRepository _userBalanceRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserService(IUserRepository userRepository, IUserBalanceRepository userBalanceRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _userBalanceRepository = userBalanceRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.GetUserByUsername(request.Username) != null)
            {
                return new AuthResponse { Success = false, Message = "Kullanıcı zaten var." };
            }

            CreatePasswordHash(request.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.Add(user);

            var userBalance = new Balance
            {
                UserId = user.Id.ToString(),
                TotalBalance = 0,
                AvailableBalance = 0,
                BlockedBalance = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _userBalanceRepository.Add(userBalance);
            await _userRepository.Save();

            return new AuthResponse { Success = true, Message = "Kayıt başarılı." };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResponse { Success = false, Message = "Geçersiz kullanıcı adı veya şifre." };
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthResponse { Success = true, Message = "Giriş başarılı.", Token = token };
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();

            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }

}
