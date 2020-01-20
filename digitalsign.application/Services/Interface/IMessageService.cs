using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.CreateModels.Message;
using digitalsign.application.Contracts.V1.InputModels.Message;
using digitalsign.application.Contracts.V1.ViewModels.Message;

namespace digitalsign.application.Services.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageViewModel>> GetAsync();
        Task<IEnumerable<MessageViewModel>> GetByUserIdAsync(Guid id);
        Task<MessageViewModel> GetAsync(Guid id);
        Task<MessageViewModel> AddAsync(Guid userId, MessageInputModel createModel);
        Task<MessageViewModel> UpdateAsync(Guid id, MessageUpdateModel model);
        Task RemoveAsync(Guid id);
    }
}
