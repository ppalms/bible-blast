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

        public async Task<PagedList<Kid>> GetKids(KidParams kidParams)
        {
            var kids = _context.Kids.AsQueryable();

            return await PagedList<Kid>.CreateAsync(kids, kidParams.PageNumber, kidParams.PageSize);
        }

        public async Task<Kid> GetKid(int id)
        {
            var kid = await _context.Kids//.Include(x => x.Parents)
                .FirstOrDefaultAsync(x => x.Id == id);
            return kid;
        }
    }
}
