using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.ViewModels.ApplicationUser;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign.common.ApiResult;
using digitalsign.common.Enumeration;
using digitalsign.domain.Domain;
using digitalsign.persistence.Context;
using digitalsign.persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace digitalsign.application.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<MessageViewModel> Get()
        {
            return new List<MessageViewModel>() {
                new MessageViewModel() {
                    Id = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Now
                }
            };
        }

        public async Task<ApiResult<MessageViewModel>> AddAsync(MessageCreateModel createModel)
        {
            var message = new Message
            {
                Guid = Guid.NewGuid(),
                Payload = createModel.Payload
            };
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            var viewModel = new MessageViewModel()
            {
                CreationDate = message.CreationDate,
                Id = message.Guid.ToString(),
                Payload = message.Payload
            };
            return ApiResult<MessageViewModel>.FromValue(viewModel);
        }

        public async Task<ApiResult<MessageViewModel>> GetAsync(Guid id)
        {
            var message = await _dbContext.Messages.SingleOrDefaultAsync(x => x.Guid.Equals(id));
            var viewModel = new MessageViewModel()
            {
                CreationDate = message.CreationDate,
                Id = message.Guid.ToString(),
                Payload = message.Payload
            };
            return ApiResult<MessageViewModel>.FromValue(viewModel);
        }
    }
}
