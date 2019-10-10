using System;
using System.Linq;
using System.Threading.Tasks;
using BibleBlast.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardRepository _repo;

        public AwardsController(IAwardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var awards = await _repo.GetAwards();

            var viewModel = awards.GroupBy(a => a.Category)
                .Select(c => new AwardCategory
                {
                    CategoryId = c.Key.Id,
                    CategoryName = c.Key.Name,
                    Awards = c.OrderBy(a => a.Ordinal)
                        .Select(a => new AwardListItem
                        {
                            AwardId = a.Id,
                            ItemDescription = a.Item.Description,
                            Requirement = a.Category.Memories.Count() == a.AwardMemories.Count()
                                ? "Complete All"
                                : string.Join(", ", a.AwardMemories
                                    .OrderBy(am => am.Memory.Name)
                                    .Select(am => am.Memory.Name)),
                            Timing = a.IsImmediate ? "Now" : "Finale",
                            Ordinal = a.Ordinal,
                        }),
                });

            return Ok(viewModel);
        }

        [HttpGet("{id}", Name = "GetAward")]
        public async Task<IActionResult> Get(int id)
        {
            var award = await _repo.GetAward(id);
            if (award == null)
            {
                return NotFound();
            }

            return Ok(award);
        }

        [HttpGet("earned")]
        public async Task<IActionResult> GetAwardsEarned([FromQuery]int categoryId, DateTime fromDate, DateTime toDate)
        {
            var awards = await _repo.GetAwardsEarned(categoryId, fromDate, toDate);

            var dto = awards.GroupBy(a => new { a.AwardId, a.CategoryId, a.ItemDescription, a.IsImmediate, a.Ordinal })
                .Select(g => new AwardEarned
                {
                    Id = g.Key.AwardId,
                    CategoryId = g.Key.CategoryId,
                    ItemDescription = g.Key.ItemDescription,
                    Timing = g.Key.IsImmediate ? "Now" : "Finale",
                    Kids = g.GroupBy(k => new { k.KidId, k.FirstName, k.LastName, k.DatePresented })
                    .Select(x => new KidAwardListItem
                    {
                        Id = x.Key.KidId,
                        FirstName = x.Key.FirstName,
                        LastName = x.Key.LastName,
                        DatePresented = x.Key.DatePresented
                    }),
                    Ordinal = g.Key.Ordinal
                });

            return Ok(dto);
        }
    }
}
