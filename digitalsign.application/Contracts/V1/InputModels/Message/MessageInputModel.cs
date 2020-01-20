using Resources;
using System.ComponentModel.DataAnnotations;

namespace digitalsign.application.Contracts.V1.InputModels.Message
{
    public class MessageInputModel
    {
        [Display(Name = "MessagePayload", ResourceType = typeof(Resource))]
        [Required]
        public string Payload { get; set; }
    }
}
