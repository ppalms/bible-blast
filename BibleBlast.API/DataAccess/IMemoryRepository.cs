using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IMemoryRepository
    {
        Task<IEnumerable<Memory>> GetMemories();
        Task<IEnumerable<MemoryCategory>> GetMemoryCategories();
        Task<IEnumerable<KidMemory>> GetCompletedMemories(CompletedMemoryParams completedMemoryParams);
    }

    public class CompletedMemoryParams : PagedListParams
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
