using digitalsign.common.Enumeration;
using Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.ViewModels.Message
{
    public class MessageViewModel {

        [Display(Name = "MessageId", ResourceType = typeof(Resource))]
        public Guid Id {get; set;}

        [Display(Name = "MessageCreationDate", ResourceType = typeof(Resource))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "MessagePayload", ResourceType = typeof(Resource))]
        public string Payload { get; set; }

        [Display(Name = "MessageUserName", ResourceType = typeof(Resource))]
        public string UserName { get; internal set; }
        public bool HasTask { get; internal set; }
        public TaskState TaskState { get; internal set; }
    }
}
