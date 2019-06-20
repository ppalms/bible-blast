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
    [Authorize]
    public class KidsController : ControllerBase
    {
        private readonly IKidRepository _repo;
        private readonly IMemoryRepository _memoryRepo;
        private readonly IMapper _mapper;

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string UserRole => User.FindFirstValue(ClaimTypes.Role);

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
            queryParams.UserRoles = new[] { UserRole };

            var kids = await _repo.GetKids(queryParams);

            var dto = _mapper.Map<IEnumerable<KidList>>(kids);

            Response.AddPagination(kids.CurrentPage, kids.PageSize, kids.TotalCount, kids.TotalPages);

            return Ok(dto);
        }

        [HttpGet("{id}", Name = "GetKid")]
        public async Task<IActionResult> Get(int id, bool includeMemories = false)
        {
            var kid = await _repo.GetKid(id, UserRole == UserRoles.Admin);
            if (kid == null)
            {
                return NotFound();
            }

            if (UserRole != UserRoles.Admin && UserRole != UserRoles.Coach && !kid.Parents.Any(p => p.UserId == UserId))
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

        [HttpPost]
        public async Task<IActionResult> InsertKid(KidInsertRequest dto)
        {
            var kid = _mapper.Map<Kid>(dto);
            kid.DateRegistered = DateTime.Now;

            var id = await _repo.InsertKid(kid);
            if (id > 0)
            {
                var newKid = await _repo.GetKid(id, UserRole == UserRoles.Admin);
                var newKidDetail = _mapper.Map<KidDetail>(kid);

                return CreatedAtRoute("GetKid", new { controller = "Kids", id = kid.Id }, newKidDetail);
            }

            return BadRequest("Failed to create new kid");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateKid(int id, KidUpdateRequest updatedKid)
        {
            var kid = await _repo.GetKid(id, UserRole == UserRoles.Admin);
            if (kid == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedKid, kid);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Failed to update kid");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKid(int id)
        {
            var kid = await _repo.GetKid(id, UserRole == UserRoles.Admin);
            if (kid == null)
            {
                return NotFound();
            }

            if (await _repo.DeleteKid(kid))
            {
                return NoContent();
            }

            return BadRequest("Failed to delete the kid");
        }

        [HttpGet("{id}/memories")]
        public async Task<IActionResult> GetCompletedMemeories(int id)
        {
            var memories = await _repo.GetCompletedMemories(id, UserId, UserRole);
            if (!memories.Any())
            {
                return NotFound();
            }

            if (UserRole != UserRoles.Admin && UserRole != UserRoles.Coach && !memories.Any(x => x.Kid.Parents.Any(p => p.UserId == UserId)))
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
            if (UserRole == UserRoles.Member)
            {
                return BadRequest();
            }

            bool userHasAccess = await _repo.UserHasAccess(id, UserId, UserRole);
            if (!userHasAccess)
            {
                return BadRequest();
            }

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
    }
}
