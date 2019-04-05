using System;
using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class KidInsertRequest
    {
        public int OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<UserDetail> Parents { get; set; }
    }
}
