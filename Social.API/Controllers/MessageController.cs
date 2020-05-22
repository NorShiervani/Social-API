using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Dtos;
using Social.API.Models;
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
        public async Task<IActionResult> GetMessageById(int id)
        {
            try
            {
                var messagesFromRepo = await _repo.GetMessageById(id);
                var messagesToDto = _mapper.Map<MessageForReturnDto>(messagesFromRepo);
                return Ok(messagesToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve message. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

        [HttpPost(Name = "CreateMessage")]
        public async Task<ActionResult> CreateMessage([FromBody] MessageToCreateDto messageToCreateDto)
        {
            try
            {
                var userConversatorFromRepo = await _repo.GetUserConversatorById(messageToCreateDto.UserConversatorId);
                 if (userConversatorFromRepo == null)
                    return BadRequest($"UserConversator with the id {userConversatorFromRepo.Id} does not exist.");
                
                Message message = new Message() {
                    Text = messageToCreateDto.Text,
                    Created = DateTime.Now,
                    UserConversator = userConversatorFromRepo
                };
                await _repo.Create(message);
                return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, message);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the comment. Exception thrown when attempting to add data to the database: {e.Message}");
            } 
        }

    }
}