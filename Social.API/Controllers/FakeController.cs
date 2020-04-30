using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social.API.Services;
using AutoMapper;
using Social.API.Dtos;
using System.Linq;
using Social.API.Models.Fake;

namespace Social.API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class FakeController : ControllerBase
    {
        private readonly IFakeRespository _repo;
        private readonly IMapper _mapper;
        public FakeController(IFakeRespository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetFakes()
        {
            var fakesFromRepo = await _repo.GetFakes();

            return Ok(fakesFromRepo);
        }

        [HttpGet("{id}", Name = "GetFake")]
        public async Task<IActionResult> GetFakeById(int id)
        {
            var fakeFromRepo = await _repo.GetFake(id);

            return Ok(fakeFromRepo);
        }


        [HttpPost]
        public async Task<ActionResult<Fake>> PosFake(Fake newFake)
        {
            _repo.PostFake(newFake);

            return CreatedAtAction(nameof(newFake), new { id = newFake.Id, name = newFake.Name}, newFake);
        }   
    }
}