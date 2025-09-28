using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreSampleContext _context;

        public CustomerRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerPrediction>> GetCustomersWithPredictionAsync()
        {
            return await _context.CustomerPredictions.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.Custid == customerId)
                .OrderByDescending(o => o.Orderdate)
                .ToListAsync();
        }
    }
}