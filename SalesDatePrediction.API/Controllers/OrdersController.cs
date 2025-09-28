using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newOrderId = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(CreateOrder), new { id = newOrderId }, new { OrderId = newOrderId });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Null argument provided");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new order");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}