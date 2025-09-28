namespace SalesDatePrediction.Application.DTOs
{
    public class CustomerOrderDto
    {
        public int OrderId { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string ShipCity { get; set; } = string.Empty;
    }
}