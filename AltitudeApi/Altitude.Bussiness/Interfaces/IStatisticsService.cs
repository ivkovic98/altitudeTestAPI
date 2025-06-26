using Altitude.Bussiness.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsResponseModel> GetStatisticsAsync();
    }
}
