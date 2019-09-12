using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IKidRepository
    {
        Task<PagedList<Kid>> GetKids(KidParams kidParams);
        Task<Kid> GetKid(int id);
        Task<Kid> GetKidWithChildEntities(int id);
        Task<int> InsertKid(Kid kid);
        Task<bool> DeleteKid(Kid kid);
        Task<IEnumerable<KidMemory>> GetCompletedMemories(int id);
        Task<bool> UpsertCompletedMemories(IEnumerable<KidMemory> kidMemories);
        Task<bool> DeleteCompletedMemories(int id, IEnumerable<int> memoryIds);
        Task<bool> SaveAll();
    }

    public class KidParams : PagedListParams
    {
        public string KidName { get; set; }
    }
}
