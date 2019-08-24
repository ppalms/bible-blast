using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoriesController : ControllerBase
    {
        private IMemoryRepository _repo;
        private IMapper _mapper;

        public MemoriesController(IMemoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var memories = await _repo.GetMemories();
            return Ok(memories);
        }

        [HttpGet("byCategory")]
        public async Task<IActionResult> GetByCategory()
        {
            var memories = await _repo.GetMemoryCategories();

            var dto = _mapper.Map<IEnumerable<KidMemoryCategory>>(memories);

            return Ok(dto);
        }
    }
}
