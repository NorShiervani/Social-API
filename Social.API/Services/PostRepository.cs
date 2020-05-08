using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context)
        {
            _context = context;
        }
        
        public async void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            var query = await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var query = await _context.Posts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Likes).ToListAsync(); 
            
            return query;
        }

        public async void PutPost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
              
            } 
        }
    }
}