using Microsoft.Extensions.Logging;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly ILogger<ShipperService> _logger;

        public ShipperService(IShipperRepository shipperRepository, ILogger<ShipperService> logger)
        {
            _shipperRepository = shipperRepository ?? throw new ArgumentNullException(nameof(shipperRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ShipperDto>> GetAllShippersAsync()
        {
            try
            {
                _logger.LogInformation("Getting all shippers");

                var shippers = await _shipperRepository.GetAllShippersAsync();

                var shipperDtos = shippers.Select(s => new ShipperDto
                {
                    ShipperId = s.Shipperid,
                    CompanyName = s.Companyname
                });

                _logger.LogInformation("Retrieved {Count} shippers", shipperDtos.Count());

                return shipperDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all shippers");
                throw;
            }
        }
    }
}