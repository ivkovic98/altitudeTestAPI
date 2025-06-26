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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private readonly AltitudeDbContext _context;

        public UserRepository(AltitudeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllActiveAsync()
        {
            return await _context.Users
                           .Where(u => !u.IsDeleted)
                           .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
