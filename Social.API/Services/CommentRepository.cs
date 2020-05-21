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
        private readonly DataContext _context;
        public CommentRepository(DataContext context, ILogger<CommentRepository> logger) : base(context, logger)
        {
            _context = context;
        }
        
        public async Task CreateComment(int postId, Comment comment)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == postId);

            if (post == null)
                throw new Exception($"Could not create comment, post with the id {postId} was not found.");

            comment.Post = post;
            await Create(comment);
        }
        public async Task UpdateComment(int commentId, Comment comment)
        {
            comment = _context.Comments.FirstOrDefault(x => x.Id == commentId);

            if (comment == null)
                throw new Exception($"Could not create comment, post with the id {commentId} was not found.");

            await Update(comment);
        }

        public async void DeleteComment(Comment comment)
        {
            await Delete(comment);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _context.Comments.ToListAsync();
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