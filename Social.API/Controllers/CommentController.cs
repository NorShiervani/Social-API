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
    [Route("api/v1.0/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        public CommentController(ICommentRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _repo = repo;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

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

        [HttpGet("{Id}", Name = "GetCommentsByPostId")]
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

        [HttpPost("post/{postId}")]
        public ActionResult<Comment> CreateComment(int postId, [FromBody] Comment comment)
        {
            try
            {
                _repo.CreateComment(postId, comment);
                return CreatedAtAction(nameof(GetCommentsByPostId), new { id = comment.Id }, comment);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the comment. Exception thrown when attempting to add data to the database: {e.Message}");
            } 
        }

        [HttpPut("{id}", Name = "UpdateCommentById")]
        public IActionResult UpdateCommentById(int id, Comment comment)
        {   
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest("Wrong commentId");
                }

                _repo.UpdateComment(id, comment);
                return CreatedAtAction(nameof(GetCommentsByPostId), new { id = comment.Id}, comment);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpDelete("{id}", Name ="DeleteCommentById")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            try
            {
                var comment = await _repo.GetCommentByPostId(id);
                if (comment == null)
                {
                    return NotFound("There was no comment with that Id");
                }

                _repo.DeleteComment(comment);
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
              new LinkDto(_urlHelper.Link(nameof(GetComments), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetCommentsByPostId), new { id = id }),
              "getCommentByPostId",
              "GET"));

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