using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.Autherization.ApplicationUser
{
    public class ApplicationUserLoginModel
    {
        [Required]
        [EmailAddress]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
