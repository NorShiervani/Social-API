using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IUserConversatorRepository : IRepository<UserConversator>
    {
        public Task<IEnumerable<UserConversator>> GetUserConversators();
        public void CreateUserConversator(UserConversator userConversator);
    }
}