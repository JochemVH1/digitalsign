using System;
using System.Collections.Generic;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign.persistence.Repository.Interface;

namespace digitalsign.application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;

        public MessageService(IMessageRepository messageRepo) {
            _messageRepo = messageRepo;
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
    }
}
