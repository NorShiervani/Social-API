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
        Task<IList<T>> GetAll<T>(params Expression<Func<T, object>>[] including) where T : class;
        Task<bool> Save();  
        Task<Post> GetPostById(int postId, params Expression<Func<Post, object>>[] including);
        Task<User> GetUserById(int userId, params Expression<Func<User, object>>[] including);
    }
}