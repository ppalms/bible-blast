using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IKidRepository
    {
        Task<PagedList<Kid>> GetKids(KidParams kidParams);
        Task<Kid> GetKid(int id, int userId);
        Task<IEnumerable<KidMemory>> GetCompletedMemories(int id, int userId);
        Task<bool> UpsertCompletedMemories(IEnumerable<KidMemory> kidMemories);
        Task<bool> DeleteCompletedMemories(int id, IEnumerable<int> memoryIds);
    }

    public class KidParams : PagedListParams
    {
        public string KidName { get; set; }
        public int UserId { get; set; }
    }
}
