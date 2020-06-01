using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;
using AutoMapper;
using Social.API.Dtos;
using Social.API.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace Social.API.Controllers
{
    [Route("api/v1.0/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _urlHelper = urlHelper;
        }
        /// <summary>
        /// Gets all the users
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /users
        ///     {
        ///         "id": 1
        ///         "UserName":"NoobMaster69"
        ///         "Password":"*******"
        ///         "Firstname": "Åke"
        ///         "Lastname": "Andersson"
        ///         "Email":"Åke.Andersson@test.com"
        ///         "IsSuspended":"false"
        ///         "Country":"Sweden"
        ///         "City":"Göteborg"
        ///         "DateRegistered":"2020-05-28"
        ///         "Birthdate":"1990-07-24"
        ///         "Likes":[]
        ///         "Posts":[]
        ///         "Comments":[]
        ///         "UserConversators":[]
        ///         "Role":"Regular"
        ///     }
        ///
        ///</remarks> 
        /// <param name="userName"></param>
        [HttpGet]
        public async Task<ActionResult<UserForReturnDto[]>> GetUsers(string userName = "")
        {
            try
            {
                var usersFromRepo = await _repo.GetUsers(userName);
                var usersToDto = _mapper.Map<UserForReturnDto[]>(usersFromRepo);
                return Ok(usersToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve users. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        /// <summary>
        /// Get a single User by Id
        /// </summary>
        ///<remarks>
        ///Sample Request: 
        ///
        ///    GET /users/1
        ///     {
        ///         "id": 1
        ///         "UserName":"NoobMaster69"
        ///         "Password":"*******"
        ///         "Firstname": "Åke"
        ///         "Lastname": "Andersson"
        ///         "Email":"Åke.Andersson@test.com"
        ///         "IsSuspended":"false"
        ///         "Country":"Sweden"
        ///         "City":"Göteborg"
        ///         "DateRegistered":"2020-05-28"
        ///         "Birthdate":"1990-07-24"
        ///         "Likes":[]
        ///         "Posts":[]
        ///         "Comments":[]
        ///         "UserConversators":[]
        ///         "Role":"Regular"
        ///     }
        ///
        ///</remarks> 
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserForReturnDto>> GetUserById(int id)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(id);
                var userToDto = _mapper.Map<UserForReturnDto>(userFromRepo);
                return Ok(ExpandSingleItem(userToDto));

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve the user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        /// <summary>
        /// Get all the posts by a single user
        /// </summary>
        ///<remarks> 
         ///Sample Request: 
        ///
        ///    GET /users/1/comments
        ///     {
        ///        "Id":1
        ///        "Text":lever livet! #perfectlife!"
        ///        "Created":"2020-05-20"
        ///        "Post":[]
        ///        "User":[]
        ///        "Likes":[]
        ///        "Comments"[]
        ///     }
        ///
        ///</remarks> 
        /// <param name="id"></param>
        [HttpGet("{id}/posts", Name = "GetPostsByUserId")]
        public async Task<ActionResult<UserForReturnDto>> GetPostsByUserId(int id)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(id);
                if(userFromRepo == null)
                {
                    return NoContent();
                }
                var userToDto = _mapper.Map<UserForReturnDto>(userFromRepo);
                return Ok(userToDto.Posts);

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }
        
        /// <summary>
        /// Get all the comments by a single user
        /// </summary>
        ///<remarks> 
         ///Sample Request: 
        ///
        ///    GET /users/1/comments
        ///     {
        ///        "Id":1
        ///        "Text":"Haha, gör kul ju!"
        ///        "Created":"2020-05-28"
        ///        "Post":[]
        ///        "User":[]
        ///     }
        ///
        ///</remarks> 
        /// <param name="id"></param>
        [HttpGet("{id}/comments", Name = "GetCommentsByUserId")]
        public async Task<ActionResult<UserForReturnDto>> GetCommentsByUserId(int id)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(id);
                if(userFromRepo == null)
                {
                    return NoContent();
                }
                var userToDto = _mapper.Map<UserForReturnDto>(userFromRepo);
                return Ok(userToDto.Comments);

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve comments. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }
        
        /// <summary>
        /// Create a new user
        /// </summary>
        ///<remarks> 
        ///Sample Request: 
        ///
        ///    POST /users
        ///     {
        ///         "id": 2
        ///         "UserName":"KalleAnka123"
        ///         "Password":"*******"
        ///         "Firstname": "Anders"
        ///         "Lastname": "Åkesson"
        ///         "Email":"Anders.Åkesson@test.com"
        ///         "IsSuspended":"false"
        ///         "Country":"Sweden"
        ///         "City":"Göteborg"
        ///         "DateRegistered":"2020-05-27"
        ///         "Birthdate":"1985-11-08"
        ///         "Likes":[]
        ///         "Posts":[]
        ///         "Comments":[]
        ///         "UserConversators":[]
        ///         "Role":"moderator"
        ///     }
        ///
        ///</remarks> 
        /// <param name="newUser"></param>
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<UserForReturnDto>> CreateUser(UserForCreateDto newUser)
        {
            try
            {   
                var user = _mapper.Map<User>(newUser);

                await _repo.Create(user);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id, name = user.Username}, _mapper.Map<UserForReturnDto>(user));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to create user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Update a single user
        /// </summary>
        ///<remarks> 
        ///Sample Request: 
        ///
        ///    PUT /users/1
        ///     {
        ///         "id": 1
        ///         "UserName":"NoobMaster69"
        ///         "Password":"*******"
        ///         "Firstname": "Åke"
        ///         "Lastname": "Andersson"
        ///         "Email":"Åke.Andersson@test.com"
        ///         "IsSuspended":"false"
        ///         "Country":"Sweden"
        ///         "City":"Göteborg"
        ///         "DateRegistered":"2020-05-28"
        ///         "Birthdate":"1990-07-24"
        ///         "Likes":[]
        ///         "Posts":[]
        ///         "Comments":[]
        ///         "UserConversators":[]
        ///         "Role":"Regular"
        ///     }
        ///
        ///</remarks> 

        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("{id}", Name = "UpdateUserById" )]
        public async Task<ActionResult<UserForReturnDto>> UpdateUserById(int id, User user)
        {   
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("Wrong userId");
                }

                _repo.Update(user);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id, name = user.Username}, _mapper.Map<UserForReturnDto>(user));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete a single user
        /// </summary> 
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if (user == null)
                {
                    return NotFound("There was no user with that Id");
                }

                _repo.Delete(user);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to delete user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

        private dynamic ExpandSingleItem(UserForReturnDto userDto)
        {
            var links = GetLinks(userDto.Id);

            var resourceToReturn = userDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetUserById), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetPostsByUserId), new { id = id }),
              "getPost",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetCommentsByUserId), new { id = id }),
              "getComment",
              "GET"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(UpdateUserById), new { id = id }),
               "updateUser",
               "PUT"));

            return links;
        }


        

    }
}