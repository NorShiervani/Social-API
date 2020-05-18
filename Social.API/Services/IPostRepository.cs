using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPostById(int id);
        public void CreatePost(Post post);
        public void PutPost(Post post);
        public void DeletePost(Post post);
    }
}