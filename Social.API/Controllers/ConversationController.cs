using System.Diagnostics;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Models;

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
            try
            {
                var conversationsFromRepo = await _repo.GetConversations();

                return Ok(conversationsFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

        [HttpGet("{id}", Name = "GetConversationById")]
        public async Task<IActionResult> GetConversationById(int id)
        {
            try
            {
                var conversationFromRepo = await _repo.GetConversationById(id);;
                return Ok(conversationFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            
        }
        [HttpPost]
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
                        $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }
        
    }
}