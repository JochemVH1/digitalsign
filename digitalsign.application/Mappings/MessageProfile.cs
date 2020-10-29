using AutoMapper;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.common.Enumeration;
using digitalsign.domain.Domain;

namespace digitalsign.application.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.FromUser.FullName ?? ""))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Guid))
                .ForMember(d => d.HasTask, opt => opt.MapFrom(s => s.Task != null))
                .ForMember(d => d.TaskState, opt => opt.MapFrom(s => s.Task == null ? TaskState.None : s.Task.State));
        }
    }
}
