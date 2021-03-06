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
        public PostRepository(DataContext context, ILogger<PostRepository> logger):base(context, logger)
        { }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync(); 
        }
    }
}