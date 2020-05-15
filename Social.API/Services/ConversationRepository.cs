using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class ConversationRepository : Repository<Conversation>,IConversationRepository
    {
        private readonly DataContext _context;
        public ConversationRepository(DataContext context, ILogger<ConversationRepository> logger) : base(context, logger)
        {
            _context = context;
        }
        public async Task<Conversation> CreateConversation(Conversation conversation)
        {
            Create(conversation);
            await Save();
            return conversation;
        }

        public async Task<Conversation> GetConversationById(int id)
        {
            var query = await _context.Conversations.FirstOrDefaultAsync(x => x.Id == id); 

            return query;
        }

        public async Task<IEnumerable<Conversation>> GetConversations()
        {
            var query = await _context.Conversations.ToListAsync(); 
            
            return query;
        }
    }
}