using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class DashboardViewModel
    {
        public int KidId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<KidMemoryCategory> Categories { get; set; }
    }
}
