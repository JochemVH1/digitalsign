using digitalsign.common.Enumeration;
using System;
using System.Collections.Generic;

namespace digitalsign.domain.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public Guid Identity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public UserRole Role {get; set;}

        public virtual IList<Message> Messages { get; set; }
        public virtual IList<Task> CreatedTasks { get; set; }
        public virtual IList<Task> CompletedTasks { get; set; }



        public User()
        {
            Messages = new List<Message>();
        }

        public void CreateMessage(string message)
        {
            Messages.Add(new Message
            {
                FromUser = this,
                Payload = message
            });
        }
    }
}
