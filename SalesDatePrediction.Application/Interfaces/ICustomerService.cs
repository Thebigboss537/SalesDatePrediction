using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerPredictionDto>> GetCustomersWithPredictionAsync();
        Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}