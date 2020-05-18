using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IRoleRepository  : IRepository<Role>
    {
        public Task<IEnumerable<Role>> GetRoles();
        public Task<Role> GetRoleById(int id);
        public void CreateRole(Role role);
        public void PutRole(Role role);
        public void DeleteRole(Role role);
    }
}