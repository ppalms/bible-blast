using System.Threading.Tasks;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
    }
}