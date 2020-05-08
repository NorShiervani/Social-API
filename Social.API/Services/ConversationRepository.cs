using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DataContext _context;
        public ConversationRepository(DataContext context)
        {
            _context = context;
        }
        public async void CreateConversation(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
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