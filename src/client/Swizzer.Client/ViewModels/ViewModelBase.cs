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
        private readonly IViewModelFacade _facade;
        private bool _isRunning;
        
        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }

        public ViewModelBase(
            IViewModelFacade viewModelFacade)
        {
            this._facade = viewModelFacade;
        }

        protected async Task DispatchCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            IsRunning = true;

            await _facade.CommandDispatcher.DispatchAsync(command);

            IsRunning = false;
        }

        protected async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider
        {
            try
            {
                IsRunning = true;

                var items = await _facade.QueryDispatcher.DispatchAsync<TQuery, TResult>(query);

                IsRunning = false;

                return items;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TDesc MapTo<TDesc>(object source)
        {
            return _facade.SwizzerMapper.MapTo<TDesc>(source);
        }

        public virtual async Task InitializeAsync(object parameter = null)
        {

        }
    }
}
