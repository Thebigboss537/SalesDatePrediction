using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerPredictionDto>> GetCustomersWithPredictionAsync();
        Task<IEnumerable<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}