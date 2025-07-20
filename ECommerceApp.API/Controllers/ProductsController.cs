namespace ECommerceApp.API.Controllers
{
    using ECommerceApp.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Products Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get available products
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productService.GetAvailableProductsAsync();

                if(!products.Any())
                {
                    return NotFound(new { message = "No products available." });
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log
                return StatusCode(500, new { message = "Failed to retrieve products.", detail = ex.Message });
            }
        }
    }
}
