using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class UserConversatorRepository : Repository<UserConversator>, IUserConversatorRepository
    {
        
        private readonly DataContext _context;
        public UserConversatorRepository(DataContext context, ILogger<UserConversatorRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public async void CreateUserConversator(UserConversator userConversator)
        {
            Create(userConversator);
            await Save();
        }
        
        public async Task<IEnumerable<UserConversator>> GetUserConversators()
        {
            var query = await _context.UserConversators.ToListAsync(); 
            
            return query;
        }
    }
}