using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1.0/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// Constructor for CommentController
        /// </summary>
        public CommentController(ICommentRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _repo = repo;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Get a single comment by Id
        /// </summary>
        [HttpGet("{Id}", Name = "GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
            var commentFromRepo = await _repo.GetById(id);            
            var commentToDto = _mapper.Map<CommentForReturnDto>(commentFromRepo);
            return Ok(ExpandSingleItem(commentToDto));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        /// <summary>
        /// Get all comments
        /// </summary>
        [HttpGet(Name = "GetComments")]
        public async Task<IActionResult> GetComments()
        {
            try
            {
            var commentsFromRepo = await _repo.GetComments();            
            var commentsToDto = _mapper.Map<CommentForReturnDto[]>(commentsFromRepo);
            var toReturn = commentsToDto.Select(x => ExpandSingleItem(x));
            return Ok(toReturn);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve comments. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        /// <summary>
        /// Get comments by Post ID
        /// </summary>
        [HttpGet("post/{Id}", Name = "GetCommentsByPostId")]
        public async Task<IActionResult> GetCommentsByPostId(int Id)
        {
            try
            {
            var commentsFromRepo = await _repo.GetCommentsByPostId(Id);
            var commentsToDto = _mapper.Map<CommentForReturnDto[]>(commentsFromRepo);
            return Ok(commentsToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }


        /// <summary>
        /// Creates a Comment.
        /// </summary>
        [HttpPost(Name = "CreateComment")]
        public async Task<ActionResult> CreateComment([FromBody] Comment comment)
        {
            try
            {
                await _repo.CreateComment(1, comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the comment. Exception thrown when attempting to add data to the database: {e.Message}");
            } 
        }

        /// <summary>
        /// Updates comment by Id
        /// </summary>
        [HttpPut("{Id}", Name = "UpdateCommentById")]
        public IActionResult UpdateCommentById(int id, Comment comment)
        {   
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest("Wrong commentId");
                }

                _repo.UpdateComment(id, comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id}, comment);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{Id}", Name ="DeleteCommentById")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            try
            {
                var comment = await _repo.GetCommentByPostId(id);
                if (comment == null)
                {
                    return NotFound("There was no comment with that Id");
                }

                await _repo.Delete(comment);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to delete comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        private dynamic ExpandSingleItem(CommentForReturnDto commentDto)
        {
            var links = GetLinks(commentDto.Id);

            var resourceToReturn = commentDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetCommentById), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(CreateComment), new { id = id }),
              "create",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(DeleteCommentById), new { id = id }),
               "delete",
               "DELETE"));

            links.Add(
            new LinkDto(_urlHelper.Link(nameof(UpdateCommentById), new { id = id }),
            "update",
            "PUT"));

            return links;
        }
    }
}