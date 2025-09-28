using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly StoreSampleContext _context;

        public ShipperRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetShipper>> GetAllShippersAsync()
        {
            return await _context.GetShippers.ToListAsync();
        }
    }
}