using Microsoft.AspNetCore.Mvc;
using Injection.Services.Interface;

namespace Injection.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            try
            {
                var (success, message) = await _orderService.CreateOrder(request, "690D5EEE-EF40-40A2-9BE4-CD8610C2692C");

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }
    }
}