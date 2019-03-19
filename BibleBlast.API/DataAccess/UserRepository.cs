using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .Include(x => x.Kids).ThenInclude(x => x.Kid)
                .Include(x => x.Organization)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
