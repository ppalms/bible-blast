using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class DashboardViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<DashboardMemory> Memories { get; set; }
    }

    public class DashboardMemory
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<KidMemoryListItem> Completed { get; set; }
    }
}
