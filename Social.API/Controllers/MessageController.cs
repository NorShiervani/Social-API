using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/conversations")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repo;
        private readonly IMapper _mapper;
        public MessageController(IMessageRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessagesByUserId()
        {
            var messagesFromRepo = await _repo.GetMessagesByUserId();

            return Ok(messagesFromRepo);
        }

    }
}