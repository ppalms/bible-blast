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
    }

    public class KidParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int UserId { get; set; }
    }
}
