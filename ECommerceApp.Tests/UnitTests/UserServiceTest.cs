namespace ECommerceApp.Tests.UnitTests
{
    using NUnit.Framework;
    using Moq;
    using FluentAssertions;
    using ECommerceApp.Application.Services;
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using System.Threading.Tasks;
    using System;

    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IUserBalanceRepository> _balanceRepoMock;
        private Mock<IJwtTokenGenerator> _jwtMock;

        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _balanceRepoMock = new Mock<IUserBalanceRepository>();
            _jwtMock = new Mock<IJwtTokenGenerator>();

            _userService = new UserService(
                _userRepoMock.Object,
                _balanceRepoMock.Object,
                _jwtMock.Object);
        }

        [Test]
        public async Task RegisterAsync_WhenUserExists_ReturnsFailure()
        {
            // Arrange
            var request = new RegisterRequest { Username = "john", Password = "123" };
            _userRepoMock.Setup(x => x.GetUserByUsername("john")).ReturnsAsync(new User());

            // Act
            var result = await _userService.RegisterAsync(request);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Kullanıcı zaten var.");
        }

        [Test]
        public async Task RegisterAsync_WhenUserIsNew_CreatesUserAndBalance()
        {
            // Arrange
            var request = new RegisterRequest { Username = "newuser", Password = "pass" };
            _userRepoMock.Setup(x => x.GetUserByUsername("newuser")).ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterAsync(request);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Kayıt başarılı.");
            _userRepoMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
            _balanceRepoMock.Verify(x => x.Add(It.IsAny<Balance>()), Times.Once);
            _userRepoMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public async Task LoginAsync_WithInvalidCredentials_ReturnsFailure()
        {
            // Arrange
            var request = new LoginRequest { Username = "unknown", Password = "wrong" };
            _userRepoMock.Setup(x => x.GetUserByUsername("unknown")).ReturnsAsync((User)null);

            // Act
            var result = await _userService.LoginAsync(request);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Geçersiz kullanıcı adı veya şifre.");
        }

        [Test]
        public async Task LoginAsync_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var password = "pass";
            var user = new User();
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            _userRepoMock.Setup(x => x.GetUserByUsername("john")).ReturnsAsync(user);
            _jwtMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token123");

            var request = new LoginRequest { Username = "john", Password = password };

            // Act
            var result = await _userService.LoginAsync(request);

            // Assert
            result.Success.Should().BeTrue();
            result.Token.Should().Be("token123");
        }
    }
}
