using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using digitalsign.application.Contracts.V1.CreateModels.Message;
using digitalsign.application.Contracts.V1.InputModels.Message;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign.common.Enumeration;
using digitalsign.domain.Domain;
using digitalsign.persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace digitalsign.application.Services
{
    public class MessageService : ServiceBase, IMessageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageService(ApplicationDbContext dbContext, IMapper mapper, ILogger<MessageService> logger) : base(logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<MessageViewModel>> GetAsync()
        {
            var result =  await _dbContext
                .Messages
                .Include(m => m.FromUser)
                .Include(m => m.Task)
                .Include(m => m.Task.CompletedUser)
                .Include(m => m.Task.CreatedUser)
                .ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<MessageViewModel>>(result);
        }

        public async Task<IEnumerable<MessageViewModel>> GetByUserIdAsync(Guid id)
        {
            try
            {
                var result = await _dbContext
                    .Messages
                    .Where(m => m.FromUser.Identity.Equals(id))
                    .Include(m => m.FromUser)
                    .Include(m => m.Task)
                    .Include(m => m.Task.CompletedUser)
                    .Include(m => m.Task.CreatedUser)
                    .ToListAsync()
                    .ConfigureAwait(false);
                return _mapper.Map<IEnumerable<MessageViewModel>>(result);
            }
            catch (Exception e)
            {
                Log(LogLevel.Error, e, e.Message);
                throw;
            }

        }

        public async Task<MessageViewModel> AddAsync(Guid userId, MessageInputModel createModel)
        {
            if (createModel is null) throw new ArgumentNullException(nameof(createModel));
            try
            {
                Log(LogLevel.Information, "Create new message");
                var user = _dbContext.Users
                    .Include(u => u.Messages)
                    .SingleOrDefault(u => u.Identity.Equals(userId));

                var message = new Message
                {
                    Guid = Guid.NewGuid(),
                    Payload = createModel.Payload,
                    FromUser = user,
                    CreationDate = DateTime.Now
                };
                if(createModel.IsTask)
                {
                    message.AddTask();
                }
                await _dbContext.Messages.AddAsync(message).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                Log(LogLevel.Information, "Done creating new message");
                return _mapper.Map<MessageViewModel>(message);
            }
            catch(Exception e)
            {
                Log(LogLevel.Error, e, e.Message);
                throw;
            }
        }

        public async Task<MessageViewModel> GetAsync(Guid id)
        {
            var message = await _dbContext
                .Messages
                .Where(x => x.Guid.Equals(id))
                .Include(m => m.FromUser)
                .Include(m => m.Task)
                .Include(m => m.Task.CompletedUser)
                .Include(m => m.Task.CreatedUser)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return _mapper.Map<MessageViewModel>(message);
        }

        public async Task<MessageViewModel> UpdateAsync(Guid id, MessageUpdateModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var message = await _dbContext
                .Messages
                .Where(x => x.Guid.Equals(id))
                .Include(m => m.FromUser)
                .Include(m => m.Task)
                .Include(m => m.Task.CompletedUser)
                .Include(m => m.Task.CreatedUser)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            message.Payload = model.Payload;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<MessageViewModel>(message);
        }

        public async System.Threading.Tasks.Task RemoveAsync(Guid id)
        {
            var message = await _dbContext
                .Messages
                .SingleOrDefaultAsync(x => x.Guid.Equals(id))
                .ConfigureAwait(false);

            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
