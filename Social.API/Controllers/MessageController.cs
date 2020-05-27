using System;
using System.Collections.Generic;
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
        private readonly IUrlHelper _urlHelper;
        public MessageController(IMessageRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _urlHelper = urlHelper;
        }

        [HttpGet("{id}", Name = "GetMessageById")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            try
            {
                var messageFromRepo = await _repo.GetMessageById(id);
                var messageToDto = _mapper.Map<MessageForReturnDto>(messageFromRepo);
                return Ok(ExpandSingleItem(messageToDto));
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
                    return NotFound($"UserConversator with the id {userConversatorFromRepo.Id} does not exist.");
                
                Message message = new Message() {
                    Text = messageToCreateDto.Text,
                    Created = DateTime.Now,
                    UserConversator = userConversatorFromRepo
                };
                await _repo.Create(message);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, message);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the comment. Exception thrown when attempting to add data to the database: {e.Message}");
            } 
            return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteMessageById")]
        public async Task<IActionResult> DeleteMessageById(int id)
        {
            try
            {
                var message = await _repo.GetMessageById(id);

                if (message == null)
                {
                    return NotFound($"Could not delete message. Message with Id {id} was not found.");
                }
                _repo.Delete(message);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the message. Exception thrown when attempting to delete data from the database: {e.Message}");
            }
            return BadRequest();
        }

        [HttpPut("{id}", Name = "UpdateMessage")]
        public async Task<ActionResult<MessageForReturnDto>> UpdateMessage(int id, MessageForReturnDto message)
        {
            try
            {
                var oldMessage = await _repo.GetMessageById(id);
                if(oldMessage == null)
                {
                    return NotFound($"We could not find any message with that Id: {id}");
                }

                var updatedMessage = _mapper.Map(message, oldMessage);
                _repo.Update(updatedMessage);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");                
            }
            return BadRequest();
        }

        private dynamic ExpandSingleItem(MessageForReturnDto messageDto)
        {
            var links = GetLinks(messageDto.Id);

            var resourceToReturn = messageDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetMessageById), new { id = id }),
              "self",
              "GET"));

            links.Add(
             new LinkDto(_urlHelper.Link(nameof(DeleteMessageById), new { id = id }),
             "delete",
             "DELETE"));

            return links;
        }
    }
}