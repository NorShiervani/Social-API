using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context, ILogger<MessageRepository> logger) : base(context, logger)
        {
            _context = context;
        }
        public async void CreateMessage(Message message)
        {
            await Create(message);
        }

        public async void DeleteMessage(Message message)
        {
            await Delete(message);
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _context.Messages.Include(m => m.UserConversator).ThenInclude(u => u.Conversation).FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _context.Messages.ToListAsync(); 
        }
    }
}