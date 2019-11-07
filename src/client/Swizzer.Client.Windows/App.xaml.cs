using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Swizzer.Client.Windows.Views;
using Prism.Mvvm;
using Swizzer.Client.ViewModels;
using Swizzer.Client.Services;
using Swizzer.Client.Validators;

namespace Swizzer.Client.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INavigationService, NavigationService>();

            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterForNavigation<LoginViewModel>();

            containerRegistry.Register<RegisterViewModelValidator>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            NavigationService.RegisterViewModel<LoginViewModel, LoginView>();
            NavigationService.RegisterViewModel<RegisterViewModel, RegisterView>();
        }
    }
}
