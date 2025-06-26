using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Models.Statistics
{
    public class StatisticsResponseModel
    {
        public int ActiveUsersCount { get; set; }
        public int ActiveProductsCount { get; set; }
        public List<CategoryCountModel> ProductsByCategory { get; set; } = new();
        public List<PriceRangeCountModel> ProductsByPriceRange { get; set; } = new();
    }
}
