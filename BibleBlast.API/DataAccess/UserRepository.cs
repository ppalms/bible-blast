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
                .Include(x => x.Kids)
                .Include(x => x.Organization)
                .FirstOrDefaultAsync(x => x.Id == id);
                
            return user;
        }
    }
}
