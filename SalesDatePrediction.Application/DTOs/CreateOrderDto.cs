using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public int ShipperId { get; set; }

        [Required]
        public required string ShipName { get; set; }

        [Required]
        public required string ShipAddress { get; set; }

        [Required]
        public required string ShipCity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public decimal Freight { get; set; }

        [Required]
        public required string ShipCountry { get; set; }

        // Order Detail
        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        public Decimal Discount { get; set; }
    }
}
