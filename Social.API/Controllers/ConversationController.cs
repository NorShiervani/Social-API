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
using Social.API.Filters;

namespace Social.API.Services
{
    [Produces("application/json")]
    [Route("api/v1.0/conversations")]
    [ApiController]
    [ApiKeyAuth]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

#region SwaggerComment
        /// <summary>
        /// Constructor for ConversationController
        /// </summary>
        #endregion
        public ConversationController(IConversationRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _urlHelper = urlHelper;
        }

#region SwaggerComment
        /// <summary>
        /// Get all Conversations
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /conversations
        ///    [
        ///    {
        ///         "Id": 1,
        ///         "ConversationName": "The cool guys!",
        ///         "UserConversators": [],
        ///    },
        ///    {
        ///         "Id": 2,
        ///         "ConversationName": "More guys!",
        ///         "UserConversators": [],
        ///    }
        ///    ]
        ///
        ///</remarks>
        #endregion
        [HttpGet(Name = "GetConversations")]
        public async Task<ActionResult<ConversationForReturnDto[]>> GetConversations()
        {
            try
            {
                var conversationsFromRepo = await _repo.GetConversations();
                if(conversationsFromRepo == null)
                {
                   return NotFound($"Could not find any conversations");
                }
                var conversationsToDto = _mapper.Map<ConversationForReturnDto[]>(conversationsFromRepo);
                return Ok(conversationsToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve conversations. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

#region SwaggerComment
        /// <summary>
        /// Get a single Conversation by Id
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /conversations/1
        ///    {
        ///         "Id": 1,
        ///         "ConversationName": "The cool guys!",
        ///         "UserConversators": [],
        ///         "links": [
        ///         {
        ///             "href": "https://localhost:5001/api/v1.0/conversations/1",
        ///             "rel": "self",
        ///             "method": "GET"
        ///         },
        ///         {
        ///             "href": "https://localhost:5001/api/v1.0/conversations",
        ///             "rel": "create",
        ///             "method": "POST"
        ///        },
        ///        {
        ///             "href": "https://localhost:5001/api/v1.0/conversations/1",
        ///             "rel": "update",
        ///             "method": "PUT"
        ///        }
        ///    ]
        ///    }
        ///
        ///</remarks>
        /// <param name="id"></param>
        #endregion
        [HttpGet("{Id}", Name = "GetConversationById")]
        public async Task<ActionResult<ConversationForReturnDto>> GetConversationById(int id)
        {
            try
            {
                var conversationFromRepo = await _repo.GetConversationById(id);
                if(conversationFromRepo == null)
                {
                   return NotFound($"Could not find any conversation with {id}.");
                }
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
        
#region SwaggerComment
        /// <summary>
        /// Get Conversations by User ID
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /conversations/user/3
        ///    {
        ///         "Id": 3,
        ///         "ConversationName": "The other guys!",
        ///         "UserConversators": []
        ///    }
        ///
        ///</remarks>
        /// <param name="id"></param>
        #endregion
        [HttpGet("user/{id}", Name = "GetConversationsByUserId")]
        public async Task<ActionResult<IEnumerable<ConversationForReturnDto>>> GetConversationsByUserId(int id)
        {
            try
            {
                var conversationFromRepo = await _repo.GetConversationsByUserId(id);
                if(conversationFromRepo == null)
                {
                   return NotFound($"Could not find any conversation for user with id: {id}.");
                }
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

#region SwaggerComment
        /// <summary>
        /// Creates a Conversation.
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    POST /conversations
        ///    {
        ///         "Id": 1,
        ///         "ConversationName": "Another guys reference maybe",
        ///         "UserConversators": []
        ///    }
        ///
        ///</remarks>
        /// <param name="conversation"></param>
        #endregion
        [HttpPost(Name = "CreateConversation")]
        public async Task<ActionResult<ConversationForReturnDto>> CreateConversation([FromBody]ConversationToCreateDto conversation)
        {
            try
            {
                Conversation conversationToCreate = new Conversation
                {
                    ConversationName = conversation.ConversationName,
                    UserConversators = conversation.UserConversators

                };
                await _repo.Create(conversationToCreate);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetConversationById), new {id = conversationToCreate.Id}, _mapper.Map<ConversationForReturnDto>(conversationToCreate));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to create conversation. Exception thrown when attempting to add data to the database: {e.Message}");
            }
            return BadRequest();
        }

#region SwaggerComment
        /// <summary>
        /// Updates Conversation by Id
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    PUT /conversations/1
        ///    {
        ///         "Id": 1,
        ///         "ConversationName": "Guys I'm changing this. Too many guys references, guys",
        ///         "UserConversators": []
        ///    }
        ///
        ///</remarks>
        /// <param name="id"></param>
        /// <param name="conversation"></param>
        #endregion
        [HttpPut("{id}", Name ="UpdateConversation")]
        public async Task<ActionResult<ConversationForReturnDto>> UpdateConversation(int id, ConversationToCreateDto conversation)
        {
            try
            {
                var existingConversation = await _repo.GetConversationById(id);
                if(existingConversation == null)
                {
                    return NotFound($"Could not find a conversation with id: {id}");
                }

                Conversation conversationToUpdate = _mapper.Map(conversation, existingConversation);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetConversationById), new {id = conversationToUpdate.Id},_mapper.Map<ConversationForReturnDto>(conversationToUpdate));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to update converation. Exception thrown when attempting to update data from the database: {e.Message}");
            }
            return BadRequest();
        }

#region SwaggerComment
        /// <summary>
        /// Deletes a specific Conversation.
        /// </summary>
        /// <param name="id"></param>
        #endregion
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
