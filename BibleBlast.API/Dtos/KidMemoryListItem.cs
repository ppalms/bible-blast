using System;

namespace BibleBlast.API.Dtos
{
    public class KidMemoryListItem
    {
        public int MemoryId { get; set; }
        public string MemoryName { get; set; }
        public string MemoryDescription { get; set; }
        public int Points { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
