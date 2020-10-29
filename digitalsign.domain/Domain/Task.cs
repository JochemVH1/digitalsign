using digitalsign.common.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace digitalsign.domain.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public virtual User CreatedUser { get; set; }

        public virtual User? CompletedUser { get; set; }

        public Guid MessageId { get; set; }

        public virtual Message Message { get; set; }

        public TaskState State { 
            get
            {
                return CompletedUser is null ? TaskState.Created : TaskState.Completed;
            }
        }
    }
}
