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
            var memories = await _context.Memories.Include(x => x.Category).ToListAsync();

            return memories;
        }
    }
}
