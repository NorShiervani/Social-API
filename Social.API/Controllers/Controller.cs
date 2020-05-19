using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{

    public class Controller<T, TRepository> : ControllerBase, IController<T> where T : class
    {
        public Task<ActionResult<T>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<T>> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<T>> Post(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<T>> Put(int id, T entity)
        {
            throw new System.NotImplementedException();
        }
    }

}