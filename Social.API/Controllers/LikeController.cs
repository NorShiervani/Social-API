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
    [Route("/api/v1.0/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repo;
        private readonly IMapper _mapper;

        public LikeController(ILikeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetLikes()
        {
            try
            {
                var likesFromRepo = await _repo.GetLikes();
                var likesToDto = _mapper.Map<LikeForReturnDto[]>(likesFromRepo);
                return Ok(likesToDto);
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
                    return BadRequest($"User with the id {userFromRepo.Id} does not exist.");
                var postFromRepo = await _repo.GetPostById(likeToCreateDto.PostId);
                if (postFromRepo == null)
                    return BadRequest($"Post with the id {postFromRepo.Id} does not exist.");


                Like like = new Like() {
                    User = userFromRepo,
                    Post = postFromRepo
                };
                await _repo.Create(like);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the like. Exception thrown when attempting to add data to the database: {e.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteLikeById")]
        public async Task<IActionResult> DeleteLikeById(int id)
        {
            try
            {
                var post = await _repo.GetById(id);

                if (post == null)
                {
                    return BadRequest($"Could not delete like. Like with Id {id} was not found.");
                }
                await _repo.Delete(post);

                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to delete the like. Exception thrown when attempting to delete data from the database: {e.Message}");
            }

        }
    }
}