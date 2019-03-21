using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class MemoryCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Memory> Memories { get; set; }
    }
}
