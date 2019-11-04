using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Mappers
{
    public class MessageMapperProfile : SwizzerMapperProfile
    {
        public MessageMapperProfile()
        {
            CreateMap<Message, MessageDto>();

            CreateMap<CreateMessageCommand, Message>();
        }
    }
}
