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

        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateLike(int userId, int postId, Like like)
        {
            try
            {
                var likeFromRepo = await _repo.GetById(postId);
                if (likeFromRepo == null)
                    return BadRequest($"Post with the id {postId} does not exist.");

               

                _repo.CreateLike(post);
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the post. Exception thrown when attempting to add data to the database: {e.Message}");
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
                _repo.DeleteLike(post);

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