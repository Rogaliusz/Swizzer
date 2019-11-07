using Prism.Ioc;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Cqrs
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider;
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IContainerProvider _containerProvider;

        public CommandDispatcher(IContainerProvider containerProvider )
        {
            this._containerProvider = containerProvider;
        }
        public Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            var handler = _containerProvider.Resolve<ICommandHandler<TCommand>>();
            return handler.HandleAsync(command);
        }
    }
}
