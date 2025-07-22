namespace ECommerceApp.API.Controllers
{
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Yeni kullanıcı kaydı
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        /// <summary>
        /// Giriş yap (token al)
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new { token = result.Token });
        }
    }
}
