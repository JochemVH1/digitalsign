using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.CreateModels.Message;
using digitalsign.application.Contracts.V1.InputModels.Message;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign.domain.Domain;
using digitalsign.persistence.Context;
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
        
        public async Task<IEnumerable<MessageViewModel>> GetAsync()
        {
            var result =  await _dbContext
                .Messages
                .Include(m => m.FromUser)
                .ToListAsync()
                .ConfigureAwait(false);
            return result.Select(x => new MessageViewModel()
            {
                CreationDate = x.CreationDate,
                Id = x.Guid.ToString(),
                Payload = x.Payload,
                UserName = x.FromUser?.FullName ?? ""
            }) ;
        }

        public async Task<IEnumerable<MessageViewModel>> GetByUserIdAsync(Guid id)
        {
            var result = await _dbContext
                .Messages
                .Where(m => m.FromUser.Identity.Equals(id))
                .Include(m => m.FromUser)
                .ToListAsync().ConfigureAwait(false);
            return result.Select(x => new MessageViewModel()
            {
                CreationDate = x.CreationDate,
                Id = x.Guid.ToString(),
                Payload = x.Payload,
                UserName = x.FromUser?.FullName ?? ""
            });
        }

        public async Task<MessageViewModel> AddAsync(Guid userId, MessageInputModel createModel)
        {
            if (createModel is null) throw new ArgumentNullException(nameof(createModel));
            try
            {
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

                await _dbContext.Messages.AddAsync(message).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                var viewModel = new MessageViewModel()
                {
                    CreationDate = message.CreationDate,
                    Id = message.Guid.ToString(),
                    Payload = message.Payload,
                    UserName = user.FullName ?? ""
                };
                return viewModel;
            }catch(Exception)
            {
                throw;
            }

        }

        public async Task<MessageViewModel> GetAsync(Guid id)
        {
            var message = await _dbContext.Messages.Include(m => m.FromUser).SingleOrDefaultAsync(x => x.Guid.Equals(id)).ConfigureAwait(false);
            return new MessageViewModel()
            {
                CreationDate = message.CreationDate,
                Id = message.Guid.ToString(),
                Payload = message.Payload,
                UserName = message.FromUser.FullName ?? ""
            };
        }

        public async Task<MessageViewModel> UpdateAsync(Guid id, MessageUpdateModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            var message = await _dbContext.Messages.SingleOrDefaultAsync(x => x.Guid.Equals(id)).ConfigureAwait(false);
            message.Payload = model.Payload;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return new MessageViewModel()
            {
                CreationDate = message.CreationDate,
                Id = message.Guid.ToString(),
                Payload = message.Payload,
                UserName = message.FromUser.FullName ?? ""
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            var message = await _dbContext.Messages.SingleOrDefaultAsync(x => x.Guid.Equals(id)).ConfigureAwait(false);
            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
