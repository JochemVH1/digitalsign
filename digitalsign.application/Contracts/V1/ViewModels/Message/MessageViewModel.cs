using Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.ViewModels.Message
{
    public class MessageViewModel {

        [Display(Name = "MessageId", ResourceType = typeof(Resource))]
        public string Id {get; set;}

        [Display(Name = "MessageCreationDate", ResourceType = typeof(Resource))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "MessagePayload", ResourceType = typeof(Resource))]
        public string Payload { get; set; }

        [Display(Name = "MessageUserName", ResourceType = typeof(Resource))]
        public string UserName { get; internal set; }
    }
}
