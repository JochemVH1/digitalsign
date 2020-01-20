using Resources;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.CreateModels.Message
{
    public class MessageUpdateModel
    {
        [Display(Name = "MessagePayload", ResourceType = typeof(Resource))]
        public string Payload { get; set; }

    }
}
