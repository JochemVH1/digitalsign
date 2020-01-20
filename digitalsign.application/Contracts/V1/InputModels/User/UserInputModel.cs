using digitalsign.common.Enumeration;
using Resources;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.CreateModels.User
{
    public class UserInputModel
    {
        [Display(Name = "UserFirstName", ResourceType = typeof(Resource))]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "UserLastName", ResourceType = typeof(Resource))]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "UserEmail", ResourceType = typeof(Resource))]
        [Required]
        public string Email { get; set; }
        [Display(Name = "UserPassword", ResourceType = typeof(Resource))]
        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User;
    }
}
