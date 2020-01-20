using digitalsign.common.Enumeration;
using Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.InputModels.User
{
    public class UserRegistrationInputModel
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

        public Guid Identity { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}
