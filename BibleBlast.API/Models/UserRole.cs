using Microsoft.AspNetCore.Identity;

namespace BibleBlast.API.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Coach = "Coach";
        public const string Member = "Member";
    }
}
