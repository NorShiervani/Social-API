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

        public async Task CreateUserConversator(UserConversator userConversator)
        {
            await Create(userConversator);
        }

        public async Task<UserConversator> GetUserConversatorById(int id)
        {
            return await _context.UserConversators.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<UserConversator>> GetUserConversators()
        {
            return await _context.UserConversators.ToListAsync(); 
        }
    }
}