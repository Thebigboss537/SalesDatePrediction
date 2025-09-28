using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreSampleContext _context;

        public ProductRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}