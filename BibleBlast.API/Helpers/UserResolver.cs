using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BibleBlast.API.Helpers
{
    public interface IUserResolver
    {
        int OrganizationId { get; }
        string UserRole { get; }
    }

    public class UserResolver : IUserResolver
    {
        public int OrganizationId { get; }
        public string UserRole { get; set; }

        public UserResolver(IHttpContextAccessor accessor)
        {
            // Admin won't be associated with an organization.
            if (int.TryParse(accessor.HttpContext?.User.FindFirstValue("organizationId"), out int organizationId))
            {
                OrganizationId = organizationId;
            }

            UserRole = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }
    }
}
