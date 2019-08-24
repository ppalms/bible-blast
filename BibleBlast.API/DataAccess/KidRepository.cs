using System;
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

        public async Task<PagedList<Kid>> GetKids(KidParams queryParams)
        {
            var kids = _context.Kids
                .Include(x => x.Organization)
                .Include(x => x.Parents).ThenInclude(x => x.User)
                .Include(x => x.CompletedMemories).ThenInclude(x => x.Memory).ThenInclude(x => x.Category)
                .AsQueryable();

            if (queryParams.UserRoles.Contains(UserRoles.Admin))
            {
                kids = kids.IgnoreQueryFilters();
            }
            else if (!queryParams.UserRoles.Contains(UserRoles.Coach))
            {
                kids = kids.Where(x => x.Parents.Any(p => p.UserId == queryParams.UserId));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.KidName))
            {
                kids = kids.Where(x => x.FirstName.Contains(queryParams.KidName, StringComparison.CurrentCultureIgnoreCase)
                     || x.LastName.Contains(queryParams.KidName, StringComparison.CurrentCultureIgnoreCase));
            }

            kids = kids.OrderBy(x => x.LastName);

            return await PagedList<Kid>.CreateAsync(kids, queryParams.PageNumber, queryParams.PageSize);
        }

        public async Task<Kid> GetKid(int id) => await _context.Kids.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Kid> GetKidWithChildEntities(int id) => await _context.Kids
                .Include(kid => kid.CompletedMemories).ThenInclude(km => km.Memory).ThenInclude(mem => mem.Category)
                .Include(kid => kid.Parents).ThenInclude(parent => parent.User).ThenInclude(parent => parent.Organization)
                .FirstOrDefaultAsync(kid => kid.Id == id);

        public async Task<int> InsertKid(Kid kid)
        {
            await _context.Kids.AddAsync(kid);

            await _context.SaveChangesAsync();

            return kid.Id;
        }

        public async Task<bool> DeleteKid(Kid kid)
        {
            _context.Kids.Remove(kid);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<KidMemory>> GetCompletedMemories(int id)
        {
            var kidMemories = _context.KidMemories.AsQueryable();

            var memories = await kidMemories.Include(x => x.Kid).ThenInclude(x => x.Parents)
                .Include(x => x.Memory).ThenInclude(x => x.Category)
                .Where(x => x.KidId == id)
                .ToListAsync();

            return memories;
        }

        public async Task<IEnumerable<KidMemory>> GetCompletedMemories(int id, DateTime fromDate, DateTime toDate, IEnumerable<int> categoryIds)
        {
            var memories = _context.KidMemories
                .Include(km => km.Memory).ThenInclude(m => m.Category)
                .Where(km => km.KidId == id)
                .Where(km => km.DateCompleted >= fromDate && km.DateCompleted <= toDate);

            if (categoryIds.Any())
            {
                memories = memories.Where(x => categoryIds.Contains(x.Memory.CategoryId));
            }

            return await memories.ToListAsync();
        }

        public async Task<bool> UpsertCompletedMemories(IEnumerable<KidMemory> kidMemories)
        {
            var memoriesToUpdate = kidMemories.Where(memory =>
                _context.KidMemories.Contains(memory)
            );

            _context.KidMemories.UpdateRange(memoriesToUpdate);

            var memoriesToInsert = kidMemories.Except(memoriesToUpdate);
            _context.KidMemories.AddRange(memoriesToInsert);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCompletedMemories(int id, IEnumerable<int> memoryIds)
        {
            var memories = _context.KidMemories.Where(x => x.KidId == id && memoryIds.Contains(x.MemoryId));

            _context.KidMemories.RemoveRange(memories);

            return await _context.SaveChangesAsync() > 0;
        }

        // todo replace repeated code
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
