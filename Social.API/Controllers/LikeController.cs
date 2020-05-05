using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Social.API.Controllers
{
    [Route("/api/v1.0/[Controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repo;
        private readonly IMapper _mapper;

        public LikeController(ILikeRepository repo, IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }
        
    }
}