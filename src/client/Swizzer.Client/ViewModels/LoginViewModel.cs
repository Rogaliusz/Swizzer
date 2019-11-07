using Prism.Commands;
using Swizzer.Client.Services;
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

        private readonly INavigationService _navigationService;

        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); } 
        public ICommand LoginCommand { get; private set; }
        public ICommand GoToRegisterCommand { get; private set; }

        public LoginViewModel(INavigationService navigationService)
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

        private void Submit()
        {
            Email = "Login In";
        }

        private bool CanSubmit()
            => true;
    }
}
