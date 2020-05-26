using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsers(string userName);
    }
}