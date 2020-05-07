using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = await _context.Users.Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
            
            return query;
        }

        public async Task<User> GetUserById(int id)
        {
            var query = await _context.Users.Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }
    }
}