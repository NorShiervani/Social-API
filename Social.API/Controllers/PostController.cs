  
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        public PostController(IUrlHelper urlHelper, IPostRepository repo, IMapper mapper)
        {
            _urlHelper = urlHelper;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreatePost(int userId, [FromBody] Post post)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(userId);
                if (userFromRepo == null)
                    return BadRequest($"User with the id {userId} does not exist.");

                post.User = userFromRepo;

                _repo.CreatePost(post);
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
                var postsFromRepo = await _repo.GetAll(x => x.User, x => x.Likes, x => x.Comments);
                var postsToDto = _mapper.Map<PostForReturnDto[]>(postsFromRepo);
                return Ok(postsToDto);
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
                var postToDto = _mapper.Map<PostForReturnDto>(postFromRepo);
                return Ok(ExpandSingleItem(postToDto));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve the post. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        private dynamic ExpandSingleItem(PostForReturnDto postDto)
        {
            var links = GetLinks(postDto.Id);

            var resourceToReturn = postDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetPostById), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(DeletePostById), new { id = id }),
              "delete",
              "DELETE"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(UpdatePostText), new { id = id, text = "" }),
               "update",
               "PUT"));

            return links;
        }

        [HttpDelete("{id}", Name = "DeletePostById")]
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

        [HttpPut("{id}", Name = "UpdatePostText")]
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
                _repo.PutPost(post);

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