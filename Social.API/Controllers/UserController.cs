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
            var usersFromRepo = await _repo.GetUsers();

            return Ok(usersFromRepo);
        }


        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var usersFromRepo = await _repo.GetUserById(id);

            return Ok(usersFromRepo);
        }

        [HttpGet("{id}/posts", Name = "GetUserPostsById")]
        public async Task<IActionResult> GetPostsByUserId(int id)
        {
            var usersFromRepo = await _repo.GetUserById(id);
            return Ok(usersFromRepo.Posts);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            _repo.CreateUser(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id, name = newUser.Username}, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById(int id, User user)
        {   
            if (id != user.Id)
            {
                return BadRequest();
            }

            _repo.UpdateUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id, name = user.Username}, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFakeById(int id)
        {
            var user = await _repo.GetUserById(id);
            
            if (user == null)
            {
                return NotFound();
            }
            _repo.DeleteUser(user);

            return NoContent();
        }
    }
}