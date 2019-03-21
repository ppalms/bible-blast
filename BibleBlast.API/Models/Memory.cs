using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Memory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Points { get; set; }
        public MemoryCategory Category { get; set; }
        public ICollection<KidMemory> KidMemories { get; set; }
    }
}
