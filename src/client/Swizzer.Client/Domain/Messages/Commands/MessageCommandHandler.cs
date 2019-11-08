using Swizzer.Client.Cqrs;
using Swizzer.Client.Web.Api;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Domain.Messages.Commands
{
    public class MessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly MessageApiHubWebService messageApiHubWebService;

        public MessageCommandHandler(MessageApiHubWebService messageApiHubWebService)
        {
            this.messageApiHubWebService = messageApiHubWebService;
        }

        public Task HandleAsync(CreateMessageCommand command)
        {
            return this.messageApiHubWebService.SendComandAsync(command);
        }
    }
}
