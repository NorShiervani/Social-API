using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.API.Services
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll() ;
        Task<T> GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> Save();
    }
}