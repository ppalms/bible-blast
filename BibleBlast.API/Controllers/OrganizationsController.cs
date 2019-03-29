using System.Threading.Tasks;
using BibleBlast.API.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationRepository _repo;

        public OrganizationsController(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizations = await _repo.GetOrganizations();

            return Ok(organizations);
        }
    }
}
