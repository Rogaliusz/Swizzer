using Microsoft.AspNetCore.SignalR;
using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swizzer.Web.Api.Hubs
{
    public abstract class SwizzerHubBase : Hub
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Guid UserId => Context.User?.Identity?.IsAuthenticated == true
            ? Guid.Parse(Context.User.Identity.Name)
            : Guid.Empty;

        public SwizzerHubBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            if (command is IAuthenticatedRequestProvider authenticated)
            {
                authenticated.RequestBy = UserId;
            }

            return _commandDispatcher.DispatchAsync(command);
        }
    }
}
