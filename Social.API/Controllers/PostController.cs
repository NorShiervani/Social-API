  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Dtos;
using Social.API.Filters;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/posts")]
    [ApiController]
    [ApiKeyAuth]
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

#region SwaggerComment
        /// <summary>
        /// Gets all Posts.
        /// </summary>
        #endregion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostForReturnDto>>> GetPosts()
        {
            try
            {
                var postsFromRepo = await _repo.GetAll(x => x.User, x => x.Likes, x => x.Comments);
                var toReturn = postsFromRepo.Select(x => ExpandSingleItem(x));
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve posts. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

#region SwaggerComment
        /// <summary>
        /// Gets a Post by Id.
        /// </summary>
        /// <param name="id"></param>
        #endregion
        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<ActionResult<PostForReturnDto>> GetPostById(int id)
        {
            try
            {
                var postFromRepo = await _repo.GetPostById(id);
                return Ok(ExpandSingleItem(postFromRepo));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to retrieve the post. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

#region SwaggerComment
        /// <summary>
        /// Creates a new Post.
        /// </summary>
        /// <param name="postToCreateDto"></param>
        #endregion
        [HttpPost(Name = "CreatePost")]
        public async Task<ActionResult<PostForReturnDto>> CreatePost([FromBody] PostToCreateDto postToCreateDto)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(postToCreateDto.UserId);
                if (userFromRepo == null)
                    return BadRequest($"User with the id {postToCreateDto.UserId} does not exist.");
                
                Post post = new Post() 
                {
                    Text = postToCreateDto.Text,
                    Created = DateTime.Now,
                    User = userFromRepo
                };

                await _repo.Create(post);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, _mapper.Map<PostForReturnDto>(post));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the post. Exception thrown when attempting to add data to the database: {e.Message}");
            }
            return BadRequest();
        }

#region SwaggerComment
        /// <summary>
        /// Deletes a post by Id.
        /// </summary>
        /// <param name="id"></param>
        #endregion
        [HttpDelete("{id}", Name = "DeletePostById")]
        public async Task<IActionResult> DeletePostById(int id)
        {
            try
            {
                var post = await _repo.GetPostById(id);

                if (post == null)
                {
                    return NotFound($"Could not delete post. Post with Id {id} was not found.");
                }
                _repo.Delete(post);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the post. Exception thrown when attempting to delete data from the database: {e.Message}");
            }
            return BadRequest();
        }

#region SwaggerComment
        /// <summary>
        /// Updates a Post by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedText"></param>
        #endregion
        [HttpPut("{id}", Name = "UpdatePostText")]
        public async Task<ActionResult<PostForReturnDto>> UpdatePostText(int id, [FromBody] string updatedText)
        {
            try 
            {
                var post = await _repo.GetPostById(id);

                if (post == null)
                {
                    return NotFound($"Could not update post. Post with Id {id} was not found.");
                }
                post.Text = updatedText;
                _repo.Update(post);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to update the post. Exception thrown when attempting to update data in the database: {e.Message}");
            }
            return BadRequest();
        }

        private dynamic ExpandSingleItem(Post post)
        {
            var links = GetLinks(post);
            PostForReturnDto postDto = _mapper.Map<PostForReturnDto>(post);

            var resourceToReturn = postDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(Post post)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetPostById), new { id = post.Id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(DeletePostById), new { id = post.Id }),
              "delete",
              "DELETE"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(UpdatePostText), new { id = post.Id }),
               "update",
               "PUT"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(CreatePost), null),
              "create",
              "POST"));
              
            return links;
        }
    }
}