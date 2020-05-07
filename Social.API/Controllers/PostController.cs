using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        public PostController(IPostRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var postsFromRepo = await _repo.GetPosts();

            return Ok(postsFromRepo);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var postFromRepo = await _repo.GetPost(id);

            return Ok(postFromRepo);
        }
    }
}