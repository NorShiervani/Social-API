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
    [Route("api/v1.0/conversations")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        public ConversationController(IConversationRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetConversations")]
        public async Task<IActionResult> GetConversations()
        {
            try
            {
                var conversationsFromRepo = await _repo.GetConversations();
                var conversationsToDto = _mapper.Map<ConversationForReturnDto[]>(conversationsFromRepo);
                var links = CreateLinksForCollection();
                var toReturn = conversationsToDto.Select(x => ExpandSingleItem(x));
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve conversations. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

        [HttpGet("{id}", Name = "GetConversationById")]
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
        
        [HttpGet("user/{id}", Name = "GetConversationsByUserId")]
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

        private List<LinkDto> CreateLinksForCollection()
        {
            var links = new List<LinkDto>();

            links.Add(
             new LinkDto(_urlHelper.Link(nameof(CreateConversation), null), "create", "POST"));

            // self 
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

        [HttpPost(Name = "CreateConversation")]
        public async Task<IActionResult> CreateConversation(Conversation conversation)
        {
            try
            {
                await _repo.CreateConversation(conversation);
                return Ok(conversation);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to create conversation. Exception thrown when attempting to add data to the database: {e.Message}");
            }
        }

        [HttpPut("{id}", Name ="UpdateConversation")]
        public async Task<IActionResult> UpdateConversation(int id, Conversation conversation)
        {
            if(id != conversation.Id)
            {
                return BadRequest();
            }
            try
            {
                await _repo.UpdateConversation(conversation);
                return Ok(conversation);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to update converation. Exception thrown when attempting to update data from the database: {e.Message}");
            }
        }
        
    }
}
