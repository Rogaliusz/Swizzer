using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.ViewModels
{
    public interface IViewModelFacade
    {
        ICommandDispatcher CommandDispatcher { get; }
        IQueryDispatcher QueryDispatcher { get; }
        ISwizzerMapper SwizzerMapper { get; }
    }

    public class ViewModelFacade : IViewModelFacade
    {
        public ICommandDispatcher CommandDispatcher { get; }
        public IQueryDispatcher QueryDispatcher { get; }
        public ISwizzerMapper SwizzerMapper { get; }

        public ViewModelFacade(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            ISwizzerMapper swizzerMapper)
        {
            CommandDispatcher = commandDispatcher;
            QueryDispatcher = queryDispatcher;
            SwizzerMapper = swizzerMapper;
        }
    }
}
