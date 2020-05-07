using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IConversationRepository
    {
        public Task<IEnumerable<Conversation>> GetConversations();
        public Task<Conversation> GetConversation(int id);
        public void CreateConversation(Conversation conversation);
    }
}