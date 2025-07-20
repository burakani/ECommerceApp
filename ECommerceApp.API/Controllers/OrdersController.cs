namespace ECommerceApp.API.Controllers
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Application.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Orders Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(string userId)
        {
            try
            {
                var orderId = await _orderService.Add(userId);

                if(string.IsNullOrEmpty(orderId))
                {
                    return BadRequest(new { message = "Failed to create order. Please try again." });
                }

                return Ok(orderId);
            }
            catch (Exception ex)
            {
                // Log
                return StatusCode(500, new { message = "Failed to create order.", detail = ex.Message });
            }
        }

        /// <summary>
        /// Complete a new order
        /// </summary>
        [HttpPost]
        [Route("{orderId}/complete")]
        public async Task<IActionResult> Complete(string orderId)
        {
            try
            {
                await _orderService.CompleteOrder(orderId);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log
                return StatusCode(500, new { message = "Failed to complete order.", detail = ex.Message });
            }
        }

        /// <summary>
        /// Cancel a new order
        /// </summary>
        [HttpPost]
        [Route("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(string orderId)
        {
            try
            {
                await _orderService.CancelOrder(orderId);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log
                return StatusCode(500, new { message = "Failed to Cancel order.", detail = ex.Message });
            }
        }
    }
}
