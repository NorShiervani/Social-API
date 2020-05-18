using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<User>> GetUsers();
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        
    }
}