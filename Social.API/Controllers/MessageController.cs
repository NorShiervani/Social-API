using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/messages")]
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

        [HttpGet("{id}", Name = "GetMessageById")]
        public async Task<IActionResult> GetMessagesByUserId(int id)
        {
            try
            {
                var messagesFromRepo = await _repo.GetMessageById(id);

                return Ok(messagesFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve message. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

    }
}