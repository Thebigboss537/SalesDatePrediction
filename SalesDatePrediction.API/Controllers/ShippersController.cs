using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService;
        private readonly ILogger<ShippersController> _logger;

        public ShippersController(IShipperService shipperService, ILogger<ShippersController> logger)
        {
            _shipperService = shipperService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippers()
        {
            try
            {
                var shippers = await _shipperService.GetAllShippersAsync();
                return Ok(shippers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all shippers");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}