using System;

namespace digitalsign.domain.Domain
{
    public class Message
    {
        private DateTime _creationDate;

        public string Guid { get; set; }
        public DateTime CreationDate => _creationDate;
        public string Payload { get; set; }

        public Message()
        {
            _creationDate = DateTime.Now;
        }
    }
}
