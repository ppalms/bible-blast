using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Family
    {
        public Family()
        {
            Kid = new HashSet<Kid>();
        }

        public int FamilyId { get; set; }
        public string DadName { get; set; }
        public string MomName { get; set; }
        public string DadCell { get; set; }
        public string MomCell { get; set; }
        public string HomePhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string NonParentName { get; set; }
        public string EmergencyPhone { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Kid> Kid { get; set; }
    }
}
