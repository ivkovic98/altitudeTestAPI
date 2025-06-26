using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models.Statistics;
using Altitude.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly AltitudeDbContext _context;

        public StatisticsService(AltitudeDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticsResponseModel> GetStatisticsAsync()
        {
            var activeUsersCount = await _context.Users.CountAsync(u => !u.IsDeleted);
            var activeProductsCount = await _context.Products.CountAsync(p => !p.IsDeleted);

            var productsByCategory = await _context.Products
                .Where(p => !p.IsDeleted)
                .GroupBy(p => p.Category)
                .Select(g => new CategoryCountModel
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var priceRanges = new List<(decimal Min, decimal Max, string Label)>
            {
                (0, 50, "0-50"),
                (50, 100, "50-100"),
                (100, 500, "100-500"),
                (500, 10000, "500+")
            };

            var productsByPriceRange = new List<PriceRangeCountModel>();

            foreach (var range in priceRanges)
            {
                var count = await _context.Products
                    .Where(p => !p.IsDeleted && p.Price >= range.Min && p.Price < range.Max)
                    .CountAsync();

                productsByPriceRange.Add(new PriceRangeCountModel
                {
                    PriceRange = range.Label,
                    Count = count
                });
            }

            return new StatisticsResponseModel
            {
                ActiveUsersCount = activeUsersCount,
                ActiveProductsCount = activeProductsCount,
                ProductsByCategory = productsByCategory,
                ProductsByPriceRange = productsByPriceRange
            };
        }
    }
}
