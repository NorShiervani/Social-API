using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetComments();
        public Task<IEnumerable<Comment>> GetCommentsByPostId(int Id);
        public void CreateComment(Comment comment);
        public void DeleteComment(Comment comment);
    }
}