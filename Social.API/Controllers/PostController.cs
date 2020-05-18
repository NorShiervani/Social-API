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
<<<<<<< HEAD
            User user = await _repo.GetUserById(userId);

            if (user == null)
                return BadRequest($"Could not create post. User with the Id '{userId}' was not found.");

            post.User = user;
            _repo.Create<Post>(post);

            if (await _repo.Save())
=======
            try
            {
                User userFromRepo = _repo.GetUserById(userId).Result;
                if (userFromRepo == null)
                    return BadRequest($"User with the id {userId} does not exist.");

                post.User = userFromRepo;

                _repo.CreatePost(post);
>>>>>>> 88711e780fef8dcecdf1df1e02e5154d2c90b213
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

        [HttpGet("{postId}", Name = "GetPostById")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            try
            {
                var postsFromRepo = await _repo.GetPostById(postId, x => x.User, x=> x.Likes);
                return Ok(postsFromRepo);
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
                _repo.Delete(post);

                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the post. Exception thrown when attempting to delete data from the database: {e.Message}");
            }

        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostText(int id, [FromBody] string updatedText)
        {
            try 
            {
                var post = await _repo.GetPostById(id);

                if (post == null)
                {
                    return BadRequest($"Could not update post. Post with Id {id} was not found.");
                }
                post.Text = updatedText;
                _repo.Update(post);

                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to update the post. Exception thrown when attempting to update data in the database: {e.Message}");
            }
        }
    }
}
