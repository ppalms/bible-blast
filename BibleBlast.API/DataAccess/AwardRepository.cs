using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.DataAccess
{
    public class AwardRepository : IAwardRepository
    {
        private readonly SqlServerAppContext _context;

        public AwardRepository(SqlServerAppContext context)
        {
            _context = context;
        }

        public async Task<Award> GetAward(int id)
        {
            return await _context.Awards.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Award>> GetAwards()
        {
            return await _context.Awards
                .Include(a => a.Item)
                .Include(a => a.AwardMemories).ThenInclude(am => am.Memory)
                    .ThenInclude(m => m.Category)
                .ToListAsync();
        }
    }
}
