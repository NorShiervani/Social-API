using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Social.API.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _context;
        private DbSet<T> table = null;
        public Repository(DataContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return table;
        }
        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id); 
        }
        public async void Create(T entity)
        {
            await table.AddAsync(entity);
        }
        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            table.Remove(entity);
        }
        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

    }
}