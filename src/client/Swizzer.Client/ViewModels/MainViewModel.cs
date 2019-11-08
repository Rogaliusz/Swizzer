using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Domain.Users.Events;
using Swizzer.Client.Mapper;
using Swizzer.Client.Services;

namespace Swizzer.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _userLoggedToken;

        public MainViewModel(
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IViewModelFacade viewModelFacade) : base(viewModelFacade)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;

            _userLoggedToken = this._eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(GoToChatAsync);
        }

        private async void GoToChatAsync()
        {
            await _navigationService.GoToAsync<ChatViewModel>();
        }

        public override async Task InitializeAsync(object parameter = null)
        {
            await base.InitializeAsync(parameter);

            await _navigationService.GoToAsync<LoginViewModel>();
        }
    }
}
