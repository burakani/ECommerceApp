namespace ECommerceApp.API.Controllers
{
    using ECommerceApp.Application.DTOs;
    using ECommerceApp.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("healthz")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Healthy");
    }
}
