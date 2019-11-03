using Autofac;
using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider;
    }
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;
        private readonly SwizzerContext _swizzerContext;

        public CommandDispatcher(
            IComponentContext componentContext,
            SwizzerContext swizzerContext)
        {
            _componentContext = componentContext;
            _swizzerContext = swizzerContext;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommandProvider
        {
            var handler = _componentContext.Resolve<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);

            await _swizzerContext.SaveChangesAsync();
        }
    }
}
