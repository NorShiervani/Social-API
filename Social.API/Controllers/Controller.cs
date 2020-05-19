using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Social.API.Models;
using Social.API.Services;
using System;
using Microsoft.AspNetCore.Http;

namespace Social.API.Controllers
{

    public class Controller<T, TRepository> : ControllerBase, IController<T> 
        where T : class
        where TRepository : Repository<T>
    {
        private readonly TRepository _repo;

        public Controller(TRepository repo)
        {
            _repo = repo;
        }

        public Task<ActionResult<T>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            try {
                var entity = await _repo.GetById(id);
                if(entity == null) 
                {
                    return NoContent();
                }
                return entity;
            }
            catch (Exception e) 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
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