using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPostById(int id);
    }
}