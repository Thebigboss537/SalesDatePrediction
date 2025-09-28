using Microsoft.Extensions.Logging;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CustomerPredictionDto>> GetCustomersWithPredictionAsync()
        {
            try
            {
                _logger.LogInformation("Getting customers with prediction");

                var customers = await _customerRepository.GetCustomersWithPredictionAsync();

                var customerDtos = customers.Select(c => new CustomerPredictionDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    LastOrderDate = c.LastOrderDate,
                    NextPredictedOrder = c.NextPredictedOrder,
                });

                _logger.LogInformation("Retrieved {Count} customers with prediction", customerDtos.Count());

                return customerDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customers with prediction");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            if (customerId <= 0)
            {
                _logger.LogWarning("Invalid customer ID: {CustomerId}", customerId);
                throw new ArgumentException("Customer ID must be greater than 0", nameof(customerId));
            }

            try
            {
                _logger.LogInformation("Getting orders for customer {CustomerId}", customerId);

                var orders = await _customerRepository.GetCustomerOrdersAsync(customerId);

                _logger.LogInformation("Retrieved {Count} orders for customer {CustomerId}", orders.Count(), customerId);

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders for customer {CustomerId}", customerId);
                throw;
            }
        }
    }
}