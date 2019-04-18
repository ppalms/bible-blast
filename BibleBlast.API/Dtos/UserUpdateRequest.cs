using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibleBlast.API.Dtos
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public string UserRole { get; set; }

        public IEnumerable<KidDetail> Kids { get; set; }
    }
}
