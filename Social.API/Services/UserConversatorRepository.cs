using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class UserConversatorRepository : IUserConversatorRepository
    {
        
        private readonly DataContext _context;
        public UserConversatorRepository(DataContext context)
        {
            _context = context;
        }

        public async void CreateUserConversator(UserConversator userConversator)
        {
            _context.UserConversators.Add(userConversator);
            await _context.SaveChangesAsync();
        }

        public async Task<UserConversator> GetUserConversator(int id)
        {
            var query = await _context.UserConversators.FirstOrDefaultAsync(x => x.Id == id); 

            return query;
        }

        public async Task<IEnumerable<UserConversator>> GetUserConversators()
        {
            var query = await _context.UserConversators.ToListAsync(); 
            
            return query;
        }
    }
}