using System.Threading.Tasks;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetUsers(PagedListParams queryParams);
        Task<User> GetUser(int id, bool ignoreQueryFilters = false);
        Task<bool> SaveAll();
    }
}
