using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsController : ControllerBase
    {
        private readonly IKidRepository _repo;
        private readonly IMemoryRepository _memoryRepo;
        private readonly IMapper _mapper;

        public KidsController(IKidRepository repo, IMemoryRepository memoryRepo, IMapper mapper)
        {
            _repo = repo;
            _memoryRepo = memoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]KidParams queryParams)
        {
            queryParams.UserId = UserId;
            // todo add roles to queryParams

            var kids = await _repo.GetKids(queryParams);

            var dto = _mapper.Map<IEnumerable<KidList>>(kids);

            Response.AddPagination(kids.CurrentPage, kids.PageSize, kids.TotalCount, kids.TotalPages);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, bool includeMemories = false)
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

            if (includeMemories)
            {
                var memoryCategories = await _memoryRepo.GetMemoryCategories();

                var dto = _mapper.Map<IEnumerable<KidMemoryCategory>>(memoryCategories);

                foreach (var completedMemory in kid.CompletedMemories)
                {
                    var memoryListItem = dto.SelectMany(x => x.Memories).First(x => x.MemoryId == completedMemory.MemoryId);
                    _mapper.Map(completedMemory, memoryListItem);
                }

                kidDetail.MemoriesByCategory = dto;
            }

            return Ok(kidDetail);
        }

        [HttpGet("{id}/memories")]
        public async Task<IActionResult> GetCompletedMemeories(int id)
        {
            var memories = await _repo.GetCompletedMemories(id, UserId);
            if (!memories.Any())
            {
                return NotFound();
            }

            if (!User.IsInRole(UserRoles.Admin) && !User.IsInRole(UserRoles.Coach) && !memories.Any(x => x.Kid.Parents.Any(p => p.UserId == UserId)))
            {
                return Unauthorized();
            }

            var completedMemeories = _mapper.Map<IEnumerable<CompletedMemory>>(memories);

            return Ok(completedMemeories);
        }

        [HttpPost("{id}/memories")]
        [Authorize(Roles = "Coach,Admin")]
        public async Task<IActionResult> UpsertCompletedMemories(int id, [FromBody]KidMemoryParams[] kidMemoryParams)
        {
            var kidMemories = _mapper.Map<IEnumerable<KidMemory>>(kidMemoryParams);
            foreach (var kidMemory in kidMemories)
            {
                kidMemory.KidId = id;
            }

            await _repo.UpsertCompletedMemories(kidMemories);

            return NoContent();
        }

        [HttpDelete("{id}/memories")]
        [Authorize(Roles = "Coach,Admin")]
        public async Task<IActionResult> DeleteCompletedMemories(int id, [FromBody]KidMemoryParams[] kidMemoryParams)
        {
            await _repo.DeleteCompletedMemories(id, kidMemoryParams.Select(x => x.MemoryId));

            return NoContent();
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
