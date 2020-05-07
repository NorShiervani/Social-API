using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/[controller]")]
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
        public async Task<IActionResult> GetCommnets()
        {
            var commentsFromRepo = await _repo.GetComments();            
            return Ok(commentsFromRepo);
        }

        [HttpGet("{Id}", Name = "{GetComments}")]
        public async Task<IActionResult> GetCommentsByPostId(int Id)
        {
            var commentsFromRepo = await _repo.GetCommentsByPostId(Id);
            return Ok(commentsFromRepo);
        }

        
    }
}