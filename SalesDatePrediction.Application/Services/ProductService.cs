using Microsoft.Extensions.Logging;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all products");

                var products = await _productRepository.GetAllProductsAsync();

                var productDtos = products.Select(p => new ProductDto
                {
                    ProductId = p.Productid,
                    ProductName = p.Productname
                });

                _logger.LogInformation("Retrieved {Count} products", productDtos.Count());

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                throw;
            }
        }
    }
}