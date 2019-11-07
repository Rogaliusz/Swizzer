using Prism.Commands;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Mapper;
using Swizzer.Client.Services;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Swizzer.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        private string _password;
        private string _error;

        private readonly INavigationService _navigationService;

        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); } 
        public string Error { get => _password; set => SetProperty(ref _error, value); }
        public ICommand LoginCommand { get; private set; }
        public ICommand GoToRegisterCommand { get; private set; }

        public LoginViewModel(
            INavigationService navigationService,
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            ISwizzerMapper swizzerMapper) : base (
                queryDispatcher, 
                commandDispatcher, 
                swizzerMapper)
        {
            LoginCommand = new DelegateCommand(Submit, CanSubmit);
            GoToRegisterCommand = new DelegateCommand(GoToRegister, CanRegister);
            
            _navigationService = navigationService;
        }

        private async void GoToRegister()
        {
            await _navigationService.GoToAsync<RegisterViewModel>();
        }

        private bool CanRegister()
        {
            return true;
        }

        private async void Submit()
        {
            try
            {
                var command = MapTo<UserLoginCommand>(this);
                await DispatchCommandAsync(command);
            } 
            catch (SwizzerClientException exception)
            {
                Error = "Incorrect credentials";
            }
        }

        private bool CanSubmit()
            => true;
    }
}
