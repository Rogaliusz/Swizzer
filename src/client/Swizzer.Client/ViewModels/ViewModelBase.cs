using Prism.Mvvm;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Mapper;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ISwizzerMapper _swizzerMapper;

        private bool _isRunning;
        
        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }

        public ViewModelBase(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            ISwizzerMapper swizzerMapper)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
            this._swizzerMapper = swizzerMapper;
        }

        protected async Task DispatchCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            IsRunning = true;

            await _commandDispatcher.DispatchAsync(command);

            IsRunning = false;
        }

        protected TDesc MapTo<TDesc>(object source)
        {
            return _swizzerMapper.MapTo<TDesc>(source);
        }

        public virtual async Task InitializeAsync(object parameter = null)
        {

        }
    }
}
