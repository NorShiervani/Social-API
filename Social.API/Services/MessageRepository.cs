using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }
        public async void CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessage(int id)
        {
            var query = await _context.Messages.Include(m => m.UserConversator).ThenInclude(u => u.Conversation).FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            var query = await _context.Messages.ToListAsync(); 
            
            return query;
        }
    }
}