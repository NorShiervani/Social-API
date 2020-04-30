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
        public ActionResult<Fake> PostFake(Fake newFake)
        {
            _repo.PostFake(newFake);
            return CreatedAtAction(nameof(GetFakeById), new { id = newFake.Id, name = newFake.Name}, newFake);
        }   

        [HttpPut("{id}")]
        public IActionResult PutFakeById(int id, Fake fake)
        {
            if (id != fake.Id)
            {
                return BadRequest();
            }

            _repo.PutFake(fake);

            return CreatedAtAction(nameof(GetFakeById), new { id = fake.Id, name = fake.Name}, fake);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFakeById(int id)
        {
            var fake = await _repo.GetFake(id);
            
            if (fake == null)
            {
                return NotFound();
            }
            _repo.DeleteFake(fake);

            return NoContent();
        }
    }
}