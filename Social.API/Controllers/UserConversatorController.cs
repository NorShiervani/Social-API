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

        /// <summary>
        /// Gets all UserConversators
        /// </summary>
        ///<remarks>
        /// Sample Request:
        /// 
        ///        GET/UserConversators
        ///          {
        ///            {
        ///             "id": 1,
        ///             "user": []
        ///             "conversation": []
        ///             "messages": null
        ///             },
        ///             {
        ///             "id": 2,
        ///             "user": []
        ///             "conversation": []
        ///             "messages": null
        ///              }
        ///     }
        ///</remarks>
        [HttpGet]
        public async Task<IActionResult> GetUserConversators()
        {
            var conversatorsFromRepo = await _repo.GetUserConversators();

            return Ok(conversatorsFromRepo);
        }

        /// <summary>
        /// Gets a UserConversator by Id.
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /UserConversator/1
        ///     {
        ///         "id": 1
        ///         "user":{ 
        ///             "id": 1
        ///             "username": "LitteJohn2038"
        ///             "password": "4321234"
        ///             "firstname": "John"
        ///             "lastname": "Doe"
        ///             "email": "jd@example.com"
        ///             "isSuspended": false
        ///             "country": "England"
        ///             "city": "Brighton"
        ///             "dateRegistered": "2020-05-22T08:53:31.7276789"
        ///             "birthdate": "2002-05-22T08:53:31.7366407"
        ///             "likes": null
        ///             "posts": null
        ///             "comments": null
        ///             "userConversators": []
        ///             "role": 0
        ///             }
        ///         "conversation": {
        ///              "id": 1,
        ///              "conversationName": "The cool guys!",
        ///              "userConversators": []
        ///          },
        ///         "messages": null
        ///     }
        ///
        ///</remarks> 
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetUserConversatorById")]
        public async Task<IActionResult> GetUserConversatorById(int id)
        {
            var conversatorFromRepo = await _repo.GetUserConversatorById(id);

            return Ok(conversatorFromRepo);
        }
    }
}