using System.Linq;
using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.DataAccess
{
    internal sealed class KidRepository : IKidRepository
    {
        private readonly SqlServerAppContext _context;

        public KidRepository(SqlServerAppContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Kid>> GetKids(KidParams queryParams)
        {
            var kids = _context.Kids.AsQueryable();
            var userRoles = _context.UserRoles.Where(x => x.UserId == queryParams.UserId).Select(x => x.Role.NormalizedName);

            if (userRoles.Contains(UserRoles.Admin))
            {
                kids = kids.IgnoreQueryFilters();
            }
            else if (!userRoles.Contains(UserRoles.Coach))
            {
                kids = kids.Where(x => x.Parents.Any(p => p.UserId == queryParams.UserId));
            }

            return await PagedList<Kid>.CreateAsync(kids, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Kid> GetKid(int id, int userId)
        {
            var kid = await _context.Kids
                .Include(k => k.Parents).ThenInclude(p => p.User).ThenInclude(p => p.Organization)
                .FirstOrDefaultAsync(x => x.Id == id);

            return kid;
        }
    }
}
