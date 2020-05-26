using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _context;
        protected readonly ILogger<Repository<T>> _logger;
        private DbSet<T> table = null;
        public Repository(DataContext _context, ILogger<Repository<T>> logger)
        {
            this._context = _context;
            table = _context.Set<T>();
            _logger = logger;
        }
        
        public async Task<IList<T>> GetAll(params Expression<Func<T, object>>[] including)
        {
            _logger.LogInformation($"Fetching entity list of type {typeof(T)} from the database.");
            var query = _context.Set<T>().AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            return await query.ToListAsync();
        }
        
        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            await table.AddAsync(entity);
            await Save();
        }

        public async Task Update(T entity)
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _context.Update(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            table.Remove(entity);
            await Save();
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public async Task<User> GetUserById(int id)
        {
            _logger.LogInformation($"Retrieving user with the id {id}.");
            return await _context.Users.Include(u => u.Posts)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> GetPostById(int id)
        {
            _logger.LogInformation($"Retrieving user with the id {id}.");
            return await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserConversator> GetUserConversatorById(int id)
        {
            _logger.LogInformation($"Retrieving userconversator with the id {id}.");
            return await _context.UserConversators.Include(u => u.User).Include(c => c.Conversation).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}