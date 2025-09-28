using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Domain.Entities;

public partial class OrderTotalsByYear
{
    public int? Orderyear { get; set; }

    public int? Qty { get; set; }
}
