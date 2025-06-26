using Altitude.Data.Context;
using Altitude.Data.Entities;
using Altitude.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AltitudeDbContext _context;
        public ProductRepository (AltitudeDbContext context): base (context)
        {
            _context = context;
        }
        public Task<IEnumerable<Product>> GetAllActiveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
