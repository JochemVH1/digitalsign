using digitalsign.common.Enumeration;
using Microsoft.AspNetCore.Identity;

namespace digitalsign.auth.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public UserRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
