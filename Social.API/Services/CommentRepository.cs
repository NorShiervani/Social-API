using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models;

namespace Social.API.Services
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var query = await _context.Comments.ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int Id)
        {
            var query = await _context.Comments.Where(x => x.Post.Id == Id).ToListAsync();
            return query;
        }
    }
}