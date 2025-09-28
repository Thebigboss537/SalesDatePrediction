using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Application.DTOs;


namespace SalesDatePrediction.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerPrediction>> GetCustomersWithPredictionAsync();
        Task<IEnumerable<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}
