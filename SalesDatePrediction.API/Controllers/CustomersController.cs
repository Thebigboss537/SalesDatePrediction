using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("predictions")]
        public async Task<IActionResult> GetCustomersWithPrediction()
        {
            try
            {
                var customers = await _customerService.GetCustomersWithPredictionAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customers with prediction");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            try
            {
                var orders = await _customerService.GetCustomerOrdersAsync(customerId);
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid customer ID: {CustomerId}", customerId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders for customer {CustomerId}", customerId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}