using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Domain.Entities;

public partial class CustomerPrediction
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime? LastOrderDate { get; set; }

    public DateTime? NextPredictedOrder { get; set; }
}
