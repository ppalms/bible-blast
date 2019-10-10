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
        public async Task<IActionResult> GetAwardsEarned([FromQuery]int categoryId)
        {
            var awards = await _repo.GetAwardsEarned(categoryId);

            var dto = awards.Select(award => new AwardEarned
            {
                Id = award.Id,
                CategoryId = award.CategoryId,
                ItemDescription = award.Item.Description,
                Timing = award.IsImmediate ? "Now" : "Finale",
                Kids = award.AwardMemories.SelectMany(am => am.Memory.KidMemories)
                    .GroupBy(km => km.Kid)
                    .Where(g => award.AwardMemories.Count() == g.Count())
                    .Select(g => new KidAwardListItem
                    {
                        Id = g.Key.Id,
                        FirstName = g.Key.FirstName,
                        LastName = g.Key.LastName,
                        DatePresented = g.Key.EarnedAwards.Any(ea => ea.AwardId == award.Id)
                            ? g.Key.EarnedAwards
                                .Where(ea => ea.AwardId == award.Id)
                                .Select(ea => ea.DatePresented)
                                .First()
                            : (DateTime?)null
                    }),
                Ordinal = award.Ordinal,
            });

            return Ok(dto);
        }
    }
}
