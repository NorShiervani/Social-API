using System.Diagnostics;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Models;
using Social.API.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Social.API.Services
{
    [Produces("application/json")]
    [Route("api/v1.0/conversations")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// Constructor for ConversationController
        /// </summary>
        public ConversationController(IConversationRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Get all Conversations
        /// </summary>
        
        [HttpGet(Name = "GetConversations")]
        public async Task<IActionResult> GetConversations()
        {
            try
            {
                var conversationsFromRepo = await _repo.GetConversations();
                var conversationsToDto = _mapper.Map<ConversationForReturnDto[]>(conversationsFromRepo);
                return Ok(conversationsToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve conversations. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }
        /// <summary>
        /// Get a single Conversation by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{Id}", Name = "GetConversationById")]
        public async Task<IActionResult> GetConversationById(int id)
        {
            try
            {
                var conversationFromRepo = await _repo.GetConversationById(id);
                var conversationToDto = _mapper.Map<ConversationForReturnDto>(conversationFromRepo);
                var links = CreateLinksForCollection();
                return Ok(ExpandSingleItem(conversationToDto));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve conversation. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            
        }
        
        /// <summary>
        /// Get Conversations by User ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("user/{Id}", Name = "GetConversationsByUserId")]
        public async Task<IActionResult> GetConversationsByUserId(int id)
        {
            try
            {
                var conversationFromRepo = await _repo.GetConversationsByUserId(id);
                var conversationToDto = _mapper.Map<ConversationForReturnDto[]>(conversationFromRepo);
                var toReturn = conversationToDto.Select(x => ExpandSingleItem(x));
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve conversation. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            
        }

        /// <summary>
        /// Creates a Conversation.
        /// </summary>
        /// <param name="conversation"></param>
        [HttpPost(Name = "CreateConversation")]
        public async Task<IActionResult> CreateConversation(Conversation conversation)
        {
            try
            {
                await _repo.Create(conversation);
                if(await _repo.Save()) {
                    return Ok(conversation);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to create conversation. Exception thrown when attempting to add data to the database: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Updates Conversation by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conversation"></param>
        [HttpPut("{id}", Name ="UpdateConversation")]
        public async Task<IActionResult> UpdateConversation(int id, Conversation conversation)
        {
            if(id != conversation.Id)
            {
                return BadRequest();
            }
            try
            {
                _repo.Update(conversation);
                
                if(await _repo.Save()) {
                    return Ok(conversation);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to update converation. Exception thrown when attempting to update data from the database: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes a specific Conversation.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}", Name ="DeleteConversation")]
        public async Task<IActionResult> DeleteConversationById(int id)
        {
            try
            {
                var conversation = await _repo.GetConversationById(id);
                if(conversation == null){

                    return NotFound($"Could not delete Conversation. Conversation with Id{id} was not found");
                }
                _repo.Delete(conversation);
                if(await _repo.Save()) {
                    return NoContent();
                }
                return NoContent();
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to delete the conversation, exception thrown when attempting to delete data from the database: {e.Message}");
            }
        }
        
        private List<LinkDto> CreateLinksForCollection()
        {
            var links = new List<LinkDto>();

            links.Add(
             new LinkDto(_urlHelper.Link(nameof(CreateConversation), null), "create", "POST"));

            links.Add(
             new LinkDto(_urlHelper.Link(nameof(GetConversations), new
             {
             }), "self", "GET"));

            links.Add(new LinkDto(_urlHelper.Link(nameof(GetConversationById), new
            {
            }), "first", "GET"));   

            return links;
        }

        private dynamic ExpandSingleItem(ConversationForReturnDto conversation)
        {
            var links = GetLinks(conversation.Id);

            var resourceToReturn = conversation.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetConversationById), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(CreateConversation), null),
              "create",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(UpdateConversation), new { id = id }),
               "update",
               "PUT"));

            return links;
        }
    }
}
