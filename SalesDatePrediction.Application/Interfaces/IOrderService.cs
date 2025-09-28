using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CreateOrderDto createOrderDto);
    }
}