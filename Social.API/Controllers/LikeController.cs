using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            var likesFromRepo = await _repo.GetLikes();
            var likesToDto = _mapper.Map<LikeForReturnDto[]>(likesFromRepo);
            return Ok(likesToDto);
        }

        [HttpGet("{Id}", Name = "GetLikesByPostId")]
        public async Task<ActionResult> GetLikesByPostId(int Id)
        {
            var likesFromRepo = await _repo.GetLikesByPostId(Id);
            var likesToDto = _mapper.Map<LikeForReturnDto[]>(likesFromRepo);
            return Ok(likesToDto);
        }

    }
}