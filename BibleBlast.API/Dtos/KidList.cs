using System;
using System.Collections.Generic;
using BibleBlast.API.Models;

namespace BibleBlast.API.Dtos
{
    public class KidList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        // todo probably don't need the whole object for the list view
        public ICollection<CompletedMemory> CompletedMemories { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
