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
    [Route("/api/v1.0/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public LikeController(ILikeRepository repo, IMapper mapper, IUrlHelper urlHelper)
        {
            _repo = repo;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetLikes")]
        public async Task<ActionResult> GetLikes()
        {
            try
            {
                var likesFromRepo = await _repo.GetLikes();
                var likesToDto = _mapper.Map<LikeForReturnDto[]>(likesFromRepo);
                var toReturn = likesToDto.Select(x => ExpandSingleItem(x));
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve likes. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }
        }

        [HttpGet("post/{Id}", Name = "GetLikesByPostId")]
        public async Task<ActionResult> GetLikesByPostId(int Id)
        {
            try
            {
                var likesFromRepo = await _repo.GetLikesByPostId(Id);
                var likesToDto = _mapper.Map<LikeForReturnDto[]>(likesFromRepo);
                return Ok(likesToDto);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Failed to retrieve likes. Exception thrown when attempting to retrieve data from the database: {e.Message}");
            }

        }

        [HttpPost(Name = "CreateLike")]
        public async Task<IActionResult> CreateLike(LikeToCreateDto likeToCreateDto)
        {
            try
            {
                var userFromRepo = await _repo.GetUserById(likeToCreateDto.UserId);
                if (userFromRepo == null)
                    return NotFound($"User with the id {userFromRepo.Id} does not exist.");
                var postFromRepo = await _repo.GetPostById(likeToCreateDto.PostId);
                if (postFromRepo == null)
                    return NotFound($"Post with the id {postFromRepo.Id} does not exist.");

                Like like = new Like() {
                    User = userFromRepo,
                    Post = postFromRepo
                };
                await _repo.Create(like);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the like. Exception thrown when attempting to add data to the database: {e.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("{id}", Name = "RemoveLikeById")]
        public async Task<IActionResult> RemoveLikeById(int id)
        {
            try
            {
                var post = await _repo.GetById(id);

                if (post == null)
                {
                    return NotFound($"Could not delete like. Like with Id {id} was not found.");
                }
                _repo.Delete(post);
                if(await _repo.Save()) {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the like. Exception thrown when attempting to delete data from the database: {e.Message}");
            }
            return BadRequest();
        }

        private dynamic ExpandSingleItem(LikeForReturnDto likeDto)
        {
            var links = GetLinks(likeDto.Id);

            var resourceToReturn = likeDto.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetLikes), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetLikesByPostId), new { id = id }),
              "getLikeByPostId",
              "GET"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(RemoveLikeById), new { id = id }),
               "delete",
               "DELETE"));
            return links;
        }
    }
}