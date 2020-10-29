using System;

namespace digitalsign.domain.Domain
{
    public class Message
    {
        public Guid Guid { get; set; }
        public DateTime CreationDate { get; set; }
        public string Payload { get; set; }
        public virtual User FromUser { get; set; }

        public virtual Task? Task { get; set; }

        public void AddTask()
        {
            Task = new Task
            {
                Message = this,
                CreatedUser = FromUser
            };
        }
    }
}
