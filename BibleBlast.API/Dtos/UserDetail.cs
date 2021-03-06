using System.Collections.Generic;
using BibleBlast.API.Models;

namespace BibleBlast.API.Dtos
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Organization Organization { get; set; }
        public string UserRole { get; set; }
        public ICollection<KidDetail> Kids { get; set; }
    }
}
