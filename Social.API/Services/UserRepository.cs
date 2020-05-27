using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;
using System.Linq;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {        
        public UserRepository(DataContext context, ILogger<UserRepository> logger) :base(context,logger)
        { }

        public async Task<IEnumerable<User>> GetUsers(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                return await _context.Users.Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
            }

            return await _context.Users.Where(u=>u.Username.ToLower() == userName.ToLower()).Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).ToListAsync();
            
        }
    }
}