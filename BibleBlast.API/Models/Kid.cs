using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Kid
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<UserKid> Parents { get; set; }
        public Organization Organization { get; set; }
        public ICollection<KidMemory> CompletedMemories { get; set; }
        public ICollection<KidAward> EarnedAwards { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
