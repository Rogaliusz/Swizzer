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
                .ForMember(x => x.Reciever, opt => opt.MapFrom(src => src.CurrentReciever.Id));

            CreateMap<CreateMessageCommand, MessageDto>()
                .ForMember(x => x.User1Id, opt => opt.MapFrom(src => src.Reciever))
                .ForMember(x => x.User2Id, opt => opt.MapFrom(src => src.RequestBy))
                .ReverseMap()
                .ForMember(x => x.Reciever, opt => opt.MapFrom(src => src.User1Id))
                .ForMember(x => x.RequestBy, opt => opt.MapFrom(src => src.User2Id));
        }
    }
}
