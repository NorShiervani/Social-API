using Microsoft.AspNetCore.Mvc;

namespace Social.API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class FakeController : ControllerBase
    {
        [HttpGet]
        public string Get() 
        {
            return "Fake works.";
        }
    }
}