using System.Collections.Generic;
using System.Linq;
using BibleBlast.API.Models;
using Newtonsoft.Json;

namespace BibleBlast.API.DataAccess
{
    public class Seeder
    {
        private readonly SqlServerAppContext _context;

        public Seeder(SqlServerAppContext context)
        {
            _context = context;
        }

        public void SeedKids()
        {
            if (_context.Kids.Any())
            {
                return;
            }

            var kidData = System.IO.File.ReadAllText("DataAccess/KidSeedData.json");
            var kids = JsonConvert.DeserializeObject<List<Kid>>(kidData);

            _context.Kids.AddRange(kids);
            _context.SaveChanges();
        }
    }
}
