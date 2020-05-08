using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class UserConversatorController : ControllerBase
    {
        private readonly IUserConversatorRepository _repo;
        private readonly IMapper _mapper;
        public UserConversatorController(IUserConversatorRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserConversators()
        {
            var conversatorsFromRepo = await _repo.GetUserConversators();

            return Ok(conversatorsFromRepo);
        }

        [HttpGet("{id}", Name = "GetUserConversatorById")]
        public async Task<IActionResult> GetUserConversatorById(int id)
        {
            var conversatorFromRepo = await _repo.GetUserConversatorById(id);

            return Ok(conversatorFromRepo);
        }
    }
}