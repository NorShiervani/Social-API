using System;
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
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        public PostController(IPostRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("{userId}")]
        public ActionResult<Post> CreatePost(int userId, [FromBody] Post post)
        {
            try
            {
                _repo.CreatePost(userId, post);
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the post. Exception thrown when attempting to add data to the database: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var postsFromRepo = await _repo.GetPosts();

                return Ok(postsFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var postFromRepo = await _repo.GetPostById(id);

                return Ok(postFromRepo);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve the post. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostById(int id)
        {
            try
            {
                var post = await _repo.GetPostById(id);

                if (post == null)
                {
                    return BadRequest($"Could not delete post. Post with Id {id} was not found.");
                }
                _repo.DeletePost(post);

                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the post. Exception thrown when attempting to delete data from the database: {e.Message}");
            }

        }
    }
}
