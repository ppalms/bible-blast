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

        public async Task<Kid> GetKid(int id, bool ignoreQueryFilters)
        {
            var kids = _context.Kids.AsQueryable();

            if (ignoreQueryFilters)
            {
                kids = kids.IgnoreQueryFilters();
            }

            var kid = await kids
                .Include(x => x.CompletedMemories).ThenInclude(x => x.Memory).ThenInclude(x => x.Category)
                .Include(k => k.Parents)
                .ThenInclude(p => p.User)
                .ThenInclude(p => p.Organization)
                .FirstOrDefaultAsync(x => x.Id == id);

            return kid;
        }

        public async Task<bool> UserHasAccess(int kidId, int userId, string role)
        {
            switch (role)
            {
                case UserRoles.Member:
                    return await _context.Kids.AnyAsync(x => x.Id == kidId && x.Parents.Any(u => u.UserId == userId));
                case UserRoles.Coach:
                    return await _context.Kids.AnyAsync(x => x.Id == kidId);
                case UserRoles.Admin:
                    return true;
                default:
                    return false;
            }
        }

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

        public async Task<IEnumerable<KidMemory>> GetCompletedMemories(int id, int userId, string role)
        {
            var kidMemories = _context.KidMemories.AsQueryable();

            if (role == UserRoles.Admin)
            {
                kidMemories = kidMemories.IgnoreQueryFilters();
            }

            var memories = await kidMemories.Include(x => x.Kid).ThenInclude(x => x.Parents)
                .Include(x => x.Memory).ThenInclude(x => x.Category)
                .Where(x => x.KidId == id)
                .ToListAsync();

            return memories;
        }

        public async Task<bool> UpsertCompletedMemories(IEnumerable<KidMemory> kidMemories)
        {
            var memoriesToUpdate = kidMemories.Where(memory =>
                _context.KidMemories.IgnoreQueryFilters().Contains(memory)
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
