using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;
        public LikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Like>> GetLikes()
        {
            var query = await _context.Likes.ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Like>> GetLikesByPostId(int Id)
        {
            var query = await _context.Likes.Where(x => x.PostId == Id).ToListAsync();
            return query;
        }
        public async void DeleteLike(Like like)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async void CreateLike(Like like)
        {
            _context.Add(like);
            await _context.SaveChangesAsync();
        }
    }
}