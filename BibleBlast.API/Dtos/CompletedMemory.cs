using System;

namespace BibleBlast.API.Dtos
{
    public class CompletedMemory
    {
        public int KidId { get; set; }
        public int MemoryId { get; set; }
        public int? Points { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
