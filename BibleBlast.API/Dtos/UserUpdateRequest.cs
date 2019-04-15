using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public int OrganizationId { get; set; }
        public IEnumerable<KidDetail> Kids { get; set; }
    }
}
