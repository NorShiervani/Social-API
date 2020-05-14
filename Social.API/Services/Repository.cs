using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Social.API.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _context;
        protected readonly ILogger<T> _logger;
        private DbSet<T> table = null;
        public Repository(DataContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task<IList<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id); 
        }
        public async void Create(T entity)
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            await table.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _context.Update(entity);
        }
        public void Delete(T entity)
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            table.Remove(entity);
        }
        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _context.SaveChangesAsync()) >= 0;
        }

    }
}