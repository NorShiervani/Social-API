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
        private readonly DataContext _context;
        public ConversationRepository(DataContext context, ILogger<ConversationRepository> logger) : base(context, logger)
        {
            _context = context;
        }
        public async Task<Conversation> CreateConversation(Conversation conversation)
        {
            await Create(conversation);
            await Save();
            return conversation;
        }

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

        public async Task<Conversation> UpdateConversation(Conversation conversation)
        {
            await Update(conversation);
            await Save();
            return conversation;
        }
    }
}