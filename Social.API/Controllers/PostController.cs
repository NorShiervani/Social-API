using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ISocialRepository _repo;
        private readonly IMapper _mapper;
        public PostController(ISocialRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<Post>> CreatePost(int userId, [FromBody] Post post)
        {
            User user = await _repo.GetUserById(userId);

            if (user == null)
                return BadRequest($"Could not create post. User with the Id '{userId}' was not found.");

            post.User = user;
            _repo.Create<Post>(post);

            if (await _repo.Save())
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);

            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Failed to save post to the database.");
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                IList<Post> postsFromRepo = await _repo.GetAll<Post>(x => x.User, x=> x.Likes);
                return Ok(postsFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve posts. Exception thrown when attempting to retrieve posts: {e.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var postsFromRepo = await _repo.GetById<Post>(id, x => x.User, x=> x.Likes);
                return Ok(postsFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to retrieve the post. Exception thrown when attempting to retrieve data from the database: {e.Message}");

            }
        }
    }
}