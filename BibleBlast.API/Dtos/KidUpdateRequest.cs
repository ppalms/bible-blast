using System;

namespace BibleBlast.API.Dtos
{
    public class KidUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsActive { get; set; }
    }
}
