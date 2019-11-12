using Swizzer.Client.Mapper;
using Swizzer.Client.ViewModels;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Domain.Messages.Mappers
{
    public class MessageProfileMapper : SwizzerMapperProfile
    {
        public MessageProfileMapper()
        {
            CreateMap<ChatViewModel, CreateMessageCommand>()
                .ForMember(x => x.Content, opt => opt.MapFrom(src => src.MessageContent))
                .ForMember(x => x.Receiver, opt => opt.MapFrom(src => src.CurrentReciever.Id));

            CreateMap<CreateMessageCommand, MessageDto>()
                .ForMember(x => x.ReceiverId, opt => opt.MapFrom(src => src.Receiver))
                .ForMember(x => x.SenderId, opt => opt.MapFrom(src => src.RequestBy))
                .ReverseMap()
                .ForMember(x => x.Receiver, opt => opt.MapFrom(src => src.ReceiverId))
                .ForMember(x => x.RequestBy, opt => opt.MapFrom(src => src.SenderId));
        }
    }
}
