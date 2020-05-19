using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{

    public class Controller<TEntity, TRepository> : ControllerBase, IController<TEntity> where TEntity : class
    {
        public Task<ActionResult<TEntity>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<TEntity>> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult<TEntity>> Put(int id, TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }

}