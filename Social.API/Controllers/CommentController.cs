using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CommentController(ICommentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
            var commentsFromRepo = await _repo.GetComments();            

            return Ok(commentsFromRepo);
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

            return Ok(commentsFromRepo);
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
    }
}