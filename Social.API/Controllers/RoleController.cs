using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social.API.Services;

namespace Social.API.Controllers
{
    [Route("api/v1.0/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
    
    private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
    }
}