using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreSampleContext _context;

        public EmployeeRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetEmployee>> GetAllEmployeesAsync()
        {
            return await _context.GetEmployees
                .Select(e => new GetEmployee
                {
                    Empid = e.Empid,
                    FullName = e.FullName,
                })
                .ToListAsync();
        }
    }
}