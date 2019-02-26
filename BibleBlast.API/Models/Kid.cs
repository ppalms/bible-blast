using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Kid
    {
        public int KidId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateRegistered { get; set; }
        public int FamilyId { get; set; }
        public bool IsMale { get; set; }

        public virtual Family Family { get; set; }
    }
}
