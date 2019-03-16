using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsController : ControllerBase
    {
        private readonly IKidRepository _repo;
        private readonly IMapper _mapper;

        public KidsController(IKidRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]KidParams queryParams)
        {
            queryParams.UserId = UserId;

            var kids = await _repo.GetKids(queryParams);

            Response.AddPagination(kids.CurrentPage, kids.PageSize, kids.TotalCount, kids.TotalPages);

            return Ok(kids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var kid = await _repo.GetKid(id, UserId);
            if (kid == null)
            {
                return NotFound();
            }

            if (!User.IsInRole(UserRoles.Admin) && !User.IsInRole(UserRoles.Coach) && !kid.Parents.Any(p => p.UserId == UserId))
            {
                return Unauthorized();
            }

            var kidDetail = _mapper.Map<KidDetail>(kid);

            return Ok(kidDetail);
        }

        [HttpGet("{id}/memories")]
        public async Task<IActionResult> GetCompletedMemeories(int id)
        {
            var memories = await _repo.GetCompletedMemories(id, UserId);

            if (!User.IsInRole(UserRoles.Admin) && !User.IsInRole(UserRoles.Coach) && !memories.Any(x => x.Kid.Parents.Any(p => p.UserId == UserId)))
            {
                return Unauthorized();
            }

            var completedMemeories = _mapper.Map<IEnumerable<CompletedMemory>>(memories);

            return Ok(completedMemeories);
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
