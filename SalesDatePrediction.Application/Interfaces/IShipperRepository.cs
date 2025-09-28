using SalesDatePrediction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<GetShipper>> GetAllShippersAsync();
    }
}
