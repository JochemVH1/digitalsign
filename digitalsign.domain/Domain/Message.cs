using System;

namespace digitalsign.domain.Domain
{
    public class Message
    {
        private readonly DateTime _creationDate;

        public Guid Guid { get; set; }
        public DateTime CreationDate => _creationDate;
        public string Payload { get; set; }

        public Message()
        {
            _creationDate = DateTime.Now;
        }
    }
}
