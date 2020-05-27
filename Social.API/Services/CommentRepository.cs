using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social.API.Models;

namespace Social.API.Services
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context, ILogger<CommentRepository> logger) : base(context, logger)
        { }
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _context.Comments.Include(x => x.Post).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int Id)
        {
            return await _context.Comments.Where(x => x.Post.Id == Id).ToListAsync();
        }
        public async Task<Comment> GetCommentByPostId(int Id)
        {
            return await _context.Comments.Where(x => x.Post.Id == Id).FirstOrDefaultAsync();
        }
    }
}