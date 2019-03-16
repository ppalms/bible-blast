using System;

namespace BibleBlast.API.Models
{
    public class KidMemory
    {
        public int KidId { get; set; }
        public int MemoryId { get; set; }
        public Kid Kid { get; set; }
        public Memory Memory { get; set; }
        public DateTime DateCompleted { get; set; } = DateTime.Today;
    }
}
