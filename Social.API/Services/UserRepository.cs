using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context, ILogger<UserRepository> logger) :base(context,logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
        }
    }
}