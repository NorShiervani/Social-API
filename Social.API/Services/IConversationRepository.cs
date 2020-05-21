using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<IEnumerable<Conversation>> GetConversations();
        Task<Conversation> GetConversationById(int id);
        Task<IEnumerable<Conversation>> GetConversationsByUserId(int id);
    }
}