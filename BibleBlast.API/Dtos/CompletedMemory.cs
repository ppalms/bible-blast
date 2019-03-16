using System;
using BibleBlast.API.Models;

namespace BibleBlast.API.Dtos
{
    public class CompletedMemory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Points { get; set; }
        public MemoryCategory Category { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
