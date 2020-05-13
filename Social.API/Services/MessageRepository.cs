using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async void CreateMessage(Message message)
        {
            Create(message);
            await Save();
        }

        public async void DeleteMessage(Message message)
        {
            Delete(message);
            await Save();
        }

        public async Task<Message> GetMessageById(int id)
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