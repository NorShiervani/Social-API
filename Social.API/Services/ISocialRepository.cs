using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface ISocialRepository
    {
        void Create<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<T> GetById<T>(int entityId, params Expression<Func<T, object>>[] including) where T : class;
        Task<IList<T>> GetAll<T>(params Expression<Func<T, object>>[] including) where T : class;
        Task<bool> Save();   
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPostById(int id);
        public Task<User> GetUserById(int id);
    }
}