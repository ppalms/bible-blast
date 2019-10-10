using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<AwardEarnedDto>> GetAwardsEarned(int categoryId, DateTime fromDate, DateTime toDate)
        {
            var categoryIdParam = new SqlParameter("@categoryId", categoryId);
            var fromDateParam = new SqlParameter("@fromDate", fromDate);
            var toDateParam = new SqlParameter("@toDate", toDate);

            var awards = _context.AwardsEarned.FromSql(@"select a.Id AwardId
    ,a.CategoryId
    ,ai.[Description] ItemDescription
    ,a.IsImmediate
    ,k.Id KidId
    ,k.FirstName
    ,k.LastName
    ,ka.DatePresented
    ,a.Ordinal
from Award a
join AwardItem ai on a.ItemId = ai.Id
join AwardMemory am on a.Id = am.AwardId
left join KidMemory km on am.MemoryId = km.MemoryId
left join Kid k on km.KidId = k.Id
left join KidAward ka on k.Id = ka.KidId and a.Id = ka.AwardId
where a.CategoryId = @categoryId
and (ka.DatePresented is null or ka.DatePresented >= @fromDate and ka.DatePresented <= @toDate)
and not exists (
    select 1
    from AwardMemory
    left join KidMemory on k.Id = KidMemory.KidId
        and AwardMemory.MemoryId = KidMemory.MemoryId
    where a.Id = AwardMemory.AwardId
    and KidMemory.MemoryId is null
)", categoryIdParam, fromDateParam, toDateParam);

            return await awards.ToListAsync();
        }
    }
}
