using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Services
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAll(params Expression<Func<T, object>>[] including);
        Task<T> GetById(int id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> Save();
        Task<User> GetUserById(int id);
        Task<Post> GetPostById(int id);
        Task<UserConversator> GetUserConversatorById(int id);


    }
}