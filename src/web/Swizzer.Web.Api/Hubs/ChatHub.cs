using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Hubs;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swizzer.Web.Api.Hubs
{
    [Authorize]
    public class ChatHub : SwizzerHubBase
    {
        private static IDictionary<Guid, string> _connections = new ConcurrentDictionary<Guid, string>();

        public ChatHub(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public override Task OnConnectedAsync()
        {
            if (!_connections.ContainsKey(UserId))
            {
                _connections.Add(UserId, Context.ConnectionId);
            } 
            else
            {
                _connections[UserId] = Context.ConnectionId;
            }
            

            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(CreateMessageCommand command)
        {
            await DispatchAsync(command);

            command.CreatedAt = DateTime.UtcNow;

            if (_connections.ContainsKey(command.Receiver))
            {
                await Clients.Client(_connections[command.Receiver]).SendAsync(Channels.Chat.Messages, command);
            }

            if (_connections.ContainsKey(command.RequestBy))
            {
                await Clients.Client(_connections[command.RequestBy]).SendAsync(Channels.Chat.Messages, command);
            }
        }
    }
}
