using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesDatePrediction.Domain.Entities;


namespace SalesDatePrediction.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerPrediction>> GetCustomersWithPredictionAsync();
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId);
    }
}
