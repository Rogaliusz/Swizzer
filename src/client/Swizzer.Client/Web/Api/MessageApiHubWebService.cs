using Swizzer.Client.Domain.Messages.Events;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Shared.Common.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Web.Api
{
    public class MessageApiHubWebService : ApiHubWebServiceBase<CreateMessageCommand, CreateMessageCommand>
    {
        public override string Channel => Channels.ChatChannel;
        public override string Method => Channels.Chat.Messages;

        public MessageApiHubWebService(IApiHubWebServiceFacade facade) : base(facade)
        {
        }

        public override async Task OnRecievedAsync(CreateMessageCommand response)
        {
            var mapped = MapTo<MessageDto>(response);
            Publish<MessageRecievedEvent, MessageDto>(mapped);
        }

        public override Task SendComandAsync(CreateMessageCommand command)
        {
            return SendCommandAsyncInternal(command);
        }
    }
}
