using System;

namespace digitalsign.application.Contracts.V1.ViewModels.Message
{
    public class MessageViewModel {
        public string Id {get; set;}
        public DateTime CreationDate { get; set; }
        public string Payload { get; set; }
    }
}
