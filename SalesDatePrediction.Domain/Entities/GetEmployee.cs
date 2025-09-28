using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Domain.Entities;

public partial class GetEmployee
{
    public int Empid { get; set; }

    public string FullName { get; set; } = null!;
}
