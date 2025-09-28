using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Domain.Entities;

public partial class GetProduct
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;
}
