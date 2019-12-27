using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.common.ApiResult;

namespace digitalsign.application.Services.Interface
{
    public interface IMessageService
    {
        IEnumerable<MessageViewModel> Get();
        Task<ApiResult<MessageViewModel>> AddAsync(MessageCreateModel createModel);
        Task<ApiResult<MessageViewModel>> GetAsync(Guid id);
    }
}
