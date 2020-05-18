using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task<IEnumerable<Comment>> GetComments();
        public Task<IEnumerable<Comment>> GetCommentsByPostId(int Id);
        public Task<Comment> GetCommentByPostId(int Id);
        public void CreateComment(int postId, Comment comment);
        public void UpdateComment(int commentId, Comment comment);
        public void DeleteComment(Comment comment);
    }
}