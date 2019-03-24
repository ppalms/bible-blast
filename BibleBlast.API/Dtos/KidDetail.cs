using System;
using System.Collections.Generic;
using BibleBlast.API.Models;

namespace BibleBlast.API.Dtos
{
    public class KidDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<UserDetail> Parents { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<CompletedMemory> CompletedMemories { get; set; }
        public IEnumerable<KidMemoryCategory> MemoriesByCategory { get; set; }
    }
}
