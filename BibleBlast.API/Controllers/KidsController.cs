using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsController : ControllerBase
    {
        private readonly IKidRepository _repo;

        public KidsController(IKidRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]KidParams kidParams)
        {
            var kids = await _repo.GetKids(kidParams);

            Response.AddPagination(kids.CurrentPage, kids.PageSize, kids.TotalCount, kids.TotalPages);

            return Ok(kids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var kid = await _repo.GetKid(id);

            return Ok(kid);
        }
    }
}
