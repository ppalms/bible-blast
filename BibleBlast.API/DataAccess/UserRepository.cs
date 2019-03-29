using System.Linq;
using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerAppContext _context;

        public UserRepository(SqlServerAppContext context)
        {
            _context = context;
        }

        public async Task<PagedList<User>> GetUsers(PagedListParams queryParams)
        {
            var users = _context.Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .Where(x => !x.UserRoles.Any(r => r.Role.Name == UserRoles.Admin))
                .AsQueryable();

            if (queryParams.UserRoles.Contains(UserRoles.Admin))
            {
                users = users.IgnoreQueryFilters();
            }

            users = users.OrderBy(x => x.LastName);

            return await PagedList<User>.CreateAsync(users, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<User> GetUser(int id, bool ignoreQueryFilters)
        {
            var users = _context.Users.AsQueryable();

            if (ignoreQueryFilters)
            {
                users = users.IgnoreQueryFilters();
            }

            var user = await users
                .Include(x => x.Kids).ThenInclude(x => x.Kid)
                .Include(x => x.Organization)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
