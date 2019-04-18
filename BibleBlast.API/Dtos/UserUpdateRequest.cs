using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int OrganizationId { get; set; }
        public string UserRole { get; set; }
        public IEnumerable<KidDetail> Kids { get; set; }
    }
}
