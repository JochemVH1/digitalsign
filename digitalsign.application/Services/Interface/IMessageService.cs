using System.Collections.Generic;
using digitalsign.application.Contracts.V1.ViewModels.Message;

namespace digitalsign.application.Services.Interface
{
    public interface IMessageService
    {
        IEnumerable<MessageViewModel> Get();
    }
}
