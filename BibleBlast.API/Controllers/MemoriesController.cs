using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string UserRole => User.FindFirstValue(ClaimTypes.Role);

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

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedMemeories([FromQuery]CompletedMemoryParams queryParams)
        {
            queryParams.UserId = UserId;
            queryParams.UserRoles = new[] { UserRole };

            if (queryParams?.FromDate > queryParams?.ToDate)
            {
                return BadRequest("Invalid date range");
            }

            var memories = await _repo.GetCompletedMemories(queryParams);

            var dto = memories
                .GroupBy(km => km.Memory.Category)
                .OrderBy(c => c.Key.Id)
                .Select(c => new DashboardViewModel
                {
                    CategoryId = c.Key.Id,
                    CategoryName = c.Key.Name,
                    Memories = c.GroupBy(km => km.Kid)
                        .OrderBy(km => km.Key.LastName)
                        .Select(k => new DashboardMemory
                        {
                            FirstName = k.Key.FirstName,
                            LastName = k.Key.LastName,
                            Completed = k.OrderByDescending(x => x.DateCompleted).ThenBy(x => x.Memory.Name)
                                .Select(x => new KidMemoryListItem
                                {
                                    MemoryId = x.Memory.Id,
                                    MemoryName = x.Memory.Name,
                                    MemoryDescription = x.Memory.Description,
                                    DateCompleted = x.DateCompleted,
                                })
                        })
                });

            return Ok(dto);
        }
    }
}
