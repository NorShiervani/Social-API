using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface ISocialRepository
    {
        void Create<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> Save();   
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPostById(int id);
        public Task<User> GetUserById(int id);
    }
}