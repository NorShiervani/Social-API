using System.Net.Sockets;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class ConversationRepository : Repository<Conversation>,IConversationRepository
    {
        public ConversationRepository(DataContext context, ILogger<ConversationRepository> logger) : base(context, logger)
        { }

        public async Task<Conversation> GetConversationById(int id)
        {
            return await _context.Conversations.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<Conversation>> GetConversations()
        {
            return await _context.Conversations.ToListAsync(); 
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByUserId(int id)
        {
            return await _context.Conversations.Where(x => x.UserConversators.Any(x => x.User.Id == id)).ToListAsync();
        }
    }
}