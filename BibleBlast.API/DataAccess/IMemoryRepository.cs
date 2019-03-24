using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IMemoryRepository
    {
        Task<IEnumerable<Memory>> GetMemories();
        Task<IEnumerable<MemoryCategory>> GetMemoryCategories();
    }
}
