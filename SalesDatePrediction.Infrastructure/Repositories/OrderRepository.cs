using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreSampleContext _context;

        public OrderRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(Order order, OrderDetail orderDetail)
        {
            // Usar el stored procedure
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_AddNewOrder @Empid={0}, @Shipperid={1}, @Shipname={2}, @Shipaddress={3}, @Shipcity={4}, @Orderdate={5}, @Requireddate={6}, @Shippeddate={7}, @Freight={8}, @Shipcountry={9}, @Custid={10}, @Productid={11}, @Unitprice={12}, @Qty={13}, @Discount={14}",
                order.Empid, order.Shipperid, order.Shipname, order.Shipaddress, order.Shipcity,
                order.Orderdate, order.Requireddate, order.Shippeddate, order.Freight, order.Shipcountry,
                order.Custid, orderDetail.Productid, orderDetail.Unitprice, orderDetail.Qty, orderDetail.Discount);

            return result;
        }
    }
}