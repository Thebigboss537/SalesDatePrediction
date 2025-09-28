using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Domain.Entities;

public partial class GetShipper
{
    public int Shipperid { get; set; }

    public string Companyname { get; set; } = null!;
}
