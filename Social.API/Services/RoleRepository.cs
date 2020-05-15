using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class RoleRepository : Repository<Role>,IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context, ILogger<RoleRepository> logger):base(context, logger)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await GetAll();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await GetById(id);
        }
        public async void CreateRole(Role role)
        {
            Create(role);
            await Save();
        }
        public async void PutRole(Role role)
        {
            Update(role);
            await Save();
        }
        public async void DeleteRole(Role role)
        {
            Delete(role);
            await Save();
        }
    }
}