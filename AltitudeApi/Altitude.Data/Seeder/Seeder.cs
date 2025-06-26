using Altitude.Common.Enums;
using Altitude.Data.Context;
using Altitude.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Altitude.Data.Seeder
{
    public class Seeder
    {
        public static async Task SeedAsync(AltitudeDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Test",
                    Email = "admin@test.com",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Number = "060123456",
                    UserRole = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
