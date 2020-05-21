using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context, ILogger<PostRepository> logger):base(context, logger)
        {
            _context = context;
        }
        
        public async void CreatePost(Post post)
        { 
            await Create(post);
        }

        public async void DeletePost(Post post)
        {
            await Delete(post);
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
        }

        public async void PutPost(Post post)
        {
            try
            {
                await Update(post);
            }
            catch (DbUpdateConcurrencyException)
            {
              
            } 
        }
    }
}