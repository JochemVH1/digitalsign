using digitalsign.common.Enumeration;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.auth.Controllers.Account
{
    public class RegisterInputModel
    {
        [Required]
        public UserRole Role { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
