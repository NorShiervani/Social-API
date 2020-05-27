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

        [HttpGet]
        public async Task<IActionResult> GetUsers(string userName = "")
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

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
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

        [HttpGet("{id}/posts", Name = "GetPostsByUserId")]
        public async Task<IActionResult> GetPostsByUserId(int id)
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
        
        [HttpGet("{id}/comments", Name = "GetCommentsByUserId")]
        public async Task<IActionResult> GetCommentsByUserId(int id)
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
        
        
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User newUser)
        {
            try
            {
                var userExist = _repo.GetUserById(newUser.Id);
                if (userExist != null)
                {
                    throw new Exception("User already exists");
                }
                
                await _repo.Create(newUser);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id, name = newUser.Username}, newUser);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to create user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

        [HttpPut("{id}", Name = "UpdateUserById" )]
        public async Task<IActionResult> UpdateUserById(int id, User user)
        {   
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("Wrong userId");
                }

                _repo.Update(user);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id, name = user.Username}, user);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

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