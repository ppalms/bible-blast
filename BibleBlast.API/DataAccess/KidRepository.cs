using System.Collections.Generic;
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

            kids = kids.OrderBy(x => x.LastName);

            return await PagedList<Kid>.CreateAsync(kids, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Kid> GetKid(int id, int userId)
        {
            var kids = _context.Kids.AsQueryable();
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.Role.NormalizedName);

            if (userRoles.Contains(UserRoles.Admin))
            {
                kids = kids.IgnoreQueryFilters();
            }

            var kid = await kids.Include(k => k.Parents)
                .ThenInclude(p => p.User)
                .ThenInclude(p => p.Organization)
                .FirstOrDefaultAsync(x => x.Id == id);

            return kid;
        }

        public async Task<IEnumerable<KidMemory>> GetCompletedMemories(int id, int userId)
        {
            var kidMemories = _context.KidMemories.AsQueryable();
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.Role.NormalizedName);

            if (userRoles.Contains(UserRoles.Admin))
            {
                kidMemories = kidMemories.IgnoreQueryFilters();
            }

            var memories = await kidMemories
                .Include(x => x.Memory).ThenInclude(x => x.Category)
                .Where(x => x.KidId == id)
                .ToListAsync();

            return memories;
        }
    }
}
