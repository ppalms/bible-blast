using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleBlast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.DataAccess
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly SqlServerAppContext _context;

        public MemoryRepository(SqlServerAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Memory>> GetMemories() =>
            await _context.Memories.ToListAsync();

        public async Task<IEnumerable<MemoryCategory>> GetMemoryCategories()
        {
            var memories = await _context.MemoryCategories
                .Include(x => x.Memories)
                .ToListAsync();

            return memories;
        }
        
        public async Task<IEnumerable<KidMemory>> GetCompletedMemories(CompletedMemoryParams queryParams)
        {
            var completedMemories = _context.KidMemories
                .Include(km => km.Kid).ThenInclude(k => k.Parents)
                .Include(km => km.Memory).ThenInclude(m => m.Category)
                .AsQueryable();

            if (queryParams.UserRoles.Contains(UserRoles.Admin))
            {
                completedMemories = completedMemories.IgnoreQueryFilters();
            }
            else if (!queryParams.UserRoles.Contains(UserRoles.Coach))
            {
                completedMemories = completedMemories.Where(x => x.Kid.Parents.Any(p => p.UserId == queryParams.UserId));
            }

            if (queryParams.FromDate != null)
            {
                completedMemories = completedMemories.Where(m => m.DateCompleted >= queryParams.FromDate);
            }

            if (queryParams.ToDate != null)
            {
                completedMemories = completedMemories.Where(m => m.DateCompleted <= queryParams.ToDate);
            }

            return await completedMemories.ToListAsync();
        }
    }
}
