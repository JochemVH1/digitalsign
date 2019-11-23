using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace digitalsign.persistence.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshTokenKey { get; set; }
        [ForeignKey(nameof(RefreshTokenKey))]
        public RefreshToken RefreshToken { get; set; }
    }
}
