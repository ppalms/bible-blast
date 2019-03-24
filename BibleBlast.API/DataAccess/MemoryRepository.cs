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

        public async Task<IEnumerable<Memory>> GetMemories()
        {
            var memories = await _context.Memories.ToListAsync();
            return memories;
        }

        public async Task<IEnumerable<MemoryCategory>> GetMemoryCategories()
        {
            var memories = await _context.MemoryCategories
                .Include(x => x.Memories).ThenInclude(x => x.KidMemories)
                .ToListAsync();

            return memories;
        }
    }
}
