using System.ComponentModel.DataAnnotations;

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
    }
}
