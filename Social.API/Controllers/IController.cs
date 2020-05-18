using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Social.API.Models;

namespace Social.API.Controllers
{

    public interface IController<T> where T : class
    {
        Task<ActionResult<T>> Get(int id);
        Task<ActionResult<T>> Delete(int id);
        Task<ActionResult<T>> Post();
        Task<ActionResult<T>> Put();
    }

}