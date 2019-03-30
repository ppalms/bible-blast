using System.Linq;
using System.Security.Claims;
using BibleBlast.API.DataAccess;
using Microsoft.AspNetCore.Http;

namespace BibleBlast.API.Helpers
{
    public interface IOrganizationProvider
    {
        int OrganizationId { get; }
    }

    public class OrganizationProvider : IOrganizationProvider
    {
        public int OrganizationId { get; }

        public OrganizationProvider(IHttpContextAccessor accessor)
        {
            if (int.TryParse(accessor.HttpContext?.User.FindFirst("organizationId")?.Value, out int organizationId))
            {
                OrganizationId = organizationId;
            }
        }
    }
}
