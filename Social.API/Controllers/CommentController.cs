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
        ///<remarks>
        ///
        ///Sample Request:
        ///
        ///     GET / comment
        ///     {
        ///         "id": 1  
        ///         "Text": "Hi there this is a sample text"
        ///         "Created":[]
        ///         "Post":[]
        ///         "User":[]
        ///     }
        ///
        ///</remarks>
        /// <param name="id"></param>
        [HttpGet("{Id}", Name = "GetCommentById")]
        public async Task<ActionResult<CommentForReturnDto>> GetCommentById(int id)
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
        ///<remarks>
        ///
        ///Sample Request:
        ///
        ///     GET / comment
        ///     {
        ///         "id": 1  
        ///         "Text": "Hi there this is a sample text"
        ///         "Created":[]
        ///         "Post":[]
        ///         "User":[]
        ///     },
        ///     {
        ///         "id": 2  
        ///         "Text": "Hi there this is a sample text"
        ///         "Created":[]
        ///         "Post":[]
        ///         "User":[]
        ///     }
        ///
        ///</remarks>
        [HttpGet(Name = "GetComments")]
        public async Task<ActionResult<CommentForReturnDto[]>> GetComments()
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
        ///<remarks>
        ///Sample Request:
        ///
        ///     GET/comments/post/{id}
        ///        [
        ///         {
        ///             "id": 1,
        ///             "text": "Cool yo!",
        ///             "created": "2020-05-22T08:53:31.7372838",
        ///             "post": null,
        ///             "user": null
        ///         },
        ///         {
        ///             "id": 3,
        ///             "text": "Uuugghhh.",
        ///             "created": "2020-05-22T08:53:31.7374273",
        ///             "post": null,
        ///             "user": null
        ///          },
        ///          {
        ///             "id": 4,
        ///             "text": "Haha awesome!",
        ///             "created": "2020-05-22T08:53:31.7374309",
        ///             "post": null,
        ///             "user": null
        ///          }
        ///         ]
        ///
        ///</remarks>
        /// <param name="Id"></param>
        [HttpGet("post/{Id}", Name = "GetCommentsByPostId")]
        public async Task<ActionResult<CommentForReturnDto[]>> GetCommentsByPostId(int Id)
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
        ///<remarks>
        ///
        ///Sample Request:
        ///
        ///     POST / comment
        ///     {
        ///         "id":  
        ///         "Text": "Hi there this is a sample text"
        ///         "Created":2018-06-11
        ///         "Post":[]
        ///         "User":[]
        ///     }
        ///
        ///</remarks>
        /// <param name="comment"></param>
        [HttpPost(Name = "CreateComment")]
        public async Task<ActionResult<CommentForReturnDto>> CreateComment([FromBody] Comment comment)
        {
            try
            {
                await _repo.Create(comment);
                if(await _repo.Save()) {
                    
                    return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, _mapper.Map<CommentForReturnDto>(comment));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the comment. Exception thrown when attempting to add data to the database: {e.Message}");
            } 
            return BadRequest();
        }

        /// <summary>
        /// Updates comment by Id
        /// </summary>
        ///<remarks>
        ///
        ///Sample Request:
        ///
        ///     PUT / comment/{id}
        ///     {
        ///         "id":  
        ///         "Text": "This is the changed text"
        ///         "Created":2018-06-11
        ///         "Post":[]
        ///         "User":[]
        ///     }
        ///
        ///</remarks>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        [HttpPut("{Id}", Name = "UpdateCommentById")]
        public async Task<ActionResult<CommentForReturnDto>> UpdateCommentById(int id, Comment comment)
        {   
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest("Wrong commentId");
                }
                _repo.Update(comment);
                if(await _repo.Save()) {
                    return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id}, _mapper.Map<CommentForReturnDto>(comment));
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to update comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes a specific comment.
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

                _repo.Delete(comment);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Failed to delete comment. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
            return BadRequest();
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