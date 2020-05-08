using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social.API.Services;
using AutoMapper;
using Social.API.Dtos;
using System.Linq;
using Social.API.Models.Fake;

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
    }
}