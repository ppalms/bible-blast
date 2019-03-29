using System.Collections.Generic;
using System.Threading.Tasks;
using BibleBlast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.DataAccess
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly SqlServerAppContext _context;
        public OrganizationRepository(SqlServerAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _context.Organizations.ToListAsync();
        }
    }
}
