using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly DataContext _context;
        public LikeRepository(DataContext context, ILogger<LikeRepository> logger) : base(context, logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _context.Likes.Include(x => x.User).Include(x => x.Post).ToListAsync();
        }
        
        public async Task<IEnumerable<Like>> GetLikesByPostId(int Id)
        {
            return await _context.Likes.Include(x => x.User).Include(x => x.Post).Where(x => x.Post.Id == Id).ToListAsync();
        }
    }
}