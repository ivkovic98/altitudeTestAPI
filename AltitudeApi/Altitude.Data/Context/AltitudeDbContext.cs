using Altitude.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Altitude.Data.Context
{
    public class AltitudeDbContext : DbContext
    {
        public AltitudeDbContext(DbContextOptions<AltitudeDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
