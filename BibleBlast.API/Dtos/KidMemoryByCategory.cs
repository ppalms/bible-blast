using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class KidMemoryCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<KidMemoryListItem> Memories { get; set; }
    }
}
