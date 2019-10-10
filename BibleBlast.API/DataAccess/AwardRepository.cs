using System.Collections.Generic;
using System.Linq;
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
                .OrderBy(a => a.Ordinal)
                .ToListAsync();
        }

        public async Task<IEnumerable<Award>> GetAwardsEarned(int categoryId)
        {
            var awards = _context.Awards.Include(a => a.Item)
                .Include(a => a.AwardMemories).ThenInclude(am => am.Memory)
                    .ThenInclude(m => m.KidMemories).ThenInclude(km => km.Kid).ThenInclude(k => k.EarnedAwards)
                .OrderBy(a => a.Ordinal)
                .Where(a => a.AwardMemories.Any(am => am.Memory.CategoryId == categoryId))
                .Where(a => !a.AwardMemories.Select(am => am.MemoryId)
                    .Except(a.AwardMemories.SelectMany(am => am.Memory.KidMemories.Select(km => km.MemoryId)))
                    .Any());

            return await awards.ToListAsync();
        }
    }
}
