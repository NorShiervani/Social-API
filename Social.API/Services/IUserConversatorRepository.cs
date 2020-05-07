using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IUserConversatorRepository
    {
        public Task<IEnumerable<UserConversator>> GetUserConversators();
        public Task<UserConversator> GetUserConversator(int id);
        public void CreateUserConversator(UserConversator userConversator);
    }
}