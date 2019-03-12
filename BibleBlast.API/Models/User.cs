using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BibleBlast.API.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserKid> Kids { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
