using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class UserConversatorRepository : Repository<UserConversator>, IUserConversatorRepository
    {
        public UserConversatorRepository(DataContext context, ILogger<UserConversatorRepository> logger) : base(context, logger)
        { }

        public async Task CreateUserConversator(UserConversator userConversator)
        {
            await Create(userConversator);
        }

        public async Task<IEnumerable<UserConversator>> GetUserConversators()
        {
            return await _context.UserConversators.ToListAsync(); 
        }
    }
}