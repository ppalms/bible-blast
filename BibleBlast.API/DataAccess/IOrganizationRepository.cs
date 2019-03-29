using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Models;

namespace BibleBlast.API.DataAccess
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetOrganizations();
    }
}
