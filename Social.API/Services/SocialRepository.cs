using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class SocialRepository : ISocialRepository
    {
        private readonly DataContext _context;
        ILoggerFactory loggerFactory = new LoggerFactory();
        protected readonly ILogger<SocialRepository> _logger;
        public SocialRepository(DataContext context, ILogger<SocialRepository> logger)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SocialRepository>();
        }

        public void Create<T>(T entity) where T : class
        {
            _logger.LogInformation($"Creating entity of type {entity.GetType()} to the datacontext.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting entity of type {entity.GetType()} to the datacontext.");
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an entity of type {entity.GetType()} in the database.");
            _context.Update(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes made in the datacontext to the database...");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Post> GetPostById(int id)
        {
            var query = await _context.Posts.Include(u => u.User).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var query = await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).ToListAsync();

            return query;
        }

        public async Task<User> GetUserById(int id)
        {
            var query = await _context.Users.Include(u => u.Posts).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }
    }
}