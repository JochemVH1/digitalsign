using digitalsign.common.Enumeration;
using System;

namespace digitalsign.application.Contracts.V1.ViewModels.User
{
    public class UserViewModel
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public Guid Identity { get; internal set; }
        public UserRole Role { get; internal set; }
    }
}
