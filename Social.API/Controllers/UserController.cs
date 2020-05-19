using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social.API.Services;
using AutoMapper;
using Social.API.Dtos;
using System.Linq;
using Social.API.Models.Fake;
using System.Net.Http;
using Social.API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;

namespace Social.API.Controllers
{
    [Route("api/v1.0/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var usersFromRepo = await _repo.GetUsers();
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
                if(userFromRepo == null)
                {
                    return NoContent();
                }
                var userToDto = _mapper.Map<UserForReturnDto>(userFromRepo);
                return Ok(userToDto);

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve the user. Exception thrown when attempting to retrieve data from the database: {e.Message}");

            }
        }

        [HttpGet("{id}/posts", Name = "GetUserPostsById")]
        public async Task<IActionResult> GetPostsByUserId(int id)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(id);
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
                var userToDto = _mapper.Map<UserForReturnDto>(userFromRepo);
                return Ok(userToDto.Comments);

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve comments. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            try
            {
                var userExist = _repo.GetUserById(newUser.Id);
                if (userExist != null)
                {
                    throw new Exception("User already exists");
                }
                _repo.CreateUser(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id, name = newUser.Username}, newUser);

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to create user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById(int id, User user)
        {   
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("Wrong userId");
                }

                _repo.UpdateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id, name = user.Username}, user);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFakeById(int id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if (user == null)
                {
                    return NotFound("There was no user with that Id");
                }

                _repo.DeleteUser(user);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to delete user. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }
    }
}