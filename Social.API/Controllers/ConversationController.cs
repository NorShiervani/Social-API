using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Social.API.Services
{
    [Route("api/v1.0/conversations")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repo;
        private readonly IMapper _mapper;
        public ConversationController(IConversationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetConversations()
        {
            var conversationsFromRepo = await _repo.GetConversations();

            return Ok(conversationsFromRepo);
        }

        [HttpGet("{id}", Name = "GetConversationById")]
        public async Task<IActionResult> GetConversationById(int id)
        {
            var conversationFromRepo = await _repo.GetConversationById(id);

            return Ok(conversationFromRepo);
        }
    }
}