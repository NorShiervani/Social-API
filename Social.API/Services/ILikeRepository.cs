using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface ILikeRepository
    {
        public Task<IEnumerable<Like>> GetLikes();
        public Task<IEnumerable<Like>> GetLikesByPostId(int Id);
        public void CreateLike(Like like);
        public void DeleteLike(Like like);
    }
}