using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IConversationRepository
    {
        public Task<IEnumerable<Conversation>> GetConversations();
        public Task<Conversation> GetConversationById(int id);
        public Task<Conversation> CreateConversation(Conversation conversation);
    }
}