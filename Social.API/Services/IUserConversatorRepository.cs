using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IUserConversatorRepository : IRepository<UserConversator>
    {
        Task<IEnumerable<UserConversator>> GetUserConversators();
        Task CreateUserConversator(UserConversator userConversator);
    }
}