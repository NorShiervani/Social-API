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

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> Delete(int id)
        {
          try
            {
                var entity = await _repo.Delete(id);
                if(entity == null)
                {
                    return NotFound();
                }
                _repo.Delete(entity);
                if(await _repo.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
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

        [HttpPost]
        public Task<ActionResult<T>> Post(T entity)
        {
            throw new System.NotImplementedException();
        }

        [HttpPut("{id}")]
        public Task<ActionResult<T>> Put(int id, T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}