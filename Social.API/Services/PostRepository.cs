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
            Create(post);
            await Save();
        }

        public async void DeletePost(Post post)
        {
            Delete(post);
            await Save();
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var query = await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
            
            return query;
        }

        public async void PutPost(Post post)
        {
            Update(post);
            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
              
            } 
        }
    }
}