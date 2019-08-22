using System.Collections.Generic;
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
                .Include(x => x.Memories)
                .ToListAsync();

            return memories;
        }
    }
}
