using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> GetMessages();
        public Task<Message> GetMessageById(int id);

        public void CreateMessage(Message message);
        public void DeleteMessage(Message message);
    }
}