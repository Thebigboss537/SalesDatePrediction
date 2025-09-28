using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Application.DTOs;
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

        public async Task<IEnumerable<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var customerIdParam = new SqlParameter("@CustomerId", customerId);

            return await _context.Database
                .SqlQueryRaw<CustomerOrderDto>("EXEC [dbo].[sp_GetClientOrders] @CustomerId", customerIdParam)
                .ToListAsync();
        }
    }
}