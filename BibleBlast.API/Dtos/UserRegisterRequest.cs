using System.ComponentModel.DataAnnotations;
using BibleBlast.API.Models;

namespace BibleBlast.API.Dtos
{
    public class UserRegisterRequest
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Your password must be at least 10 characters long")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrganizationId { get; set; }
    }
}
