using Microsoft.Extensions.Logging;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
                throw new ArgumentNullException(nameof(createOrderDto));

            try
            {
                _logger.LogInformation("Creating new order for customer {CustomerId}", createOrderDto.CustomerId);

                var order = new Order
                {
                    Custid = createOrderDto.CustomerId,
                    Empid = createOrderDto.EmpId,
                    Shipperid = createOrderDto.ShipperId,
                    Shipname = createOrderDto.ShipName,
                    Shipaddress = createOrderDto.ShipAddress,
                    Shipcity = createOrderDto.ShipCity,
                    Orderdate = createOrderDto.OrderDate,
                    Requireddate = createOrderDto.RequiredDate,
                    Shippeddate = createOrderDto.ShippedDate,
                    Freight = createOrderDto.Freight,
                    Shipcountry = createOrderDto.ShipCountry
                };

                var orderDetail = new OrderDetail
                {
                    Productid = createOrderDto.ProductId,
                    Unitprice = createOrderDto.UnitPrice,
                    Qty = createOrderDto.Quantity,
                    Discount = createOrderDto.Discount
                };

                var newOrderId = await _orderRepository.CreateOrderAsync(order, orderDetail);

                _logger.LogInformation("Successfully created order with ID {OrderId}", newOrderId);

                return newOrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new order for customer {CustomerId}", createOrderDto.CustomerId);
                throw;
            }
        }
    }
}