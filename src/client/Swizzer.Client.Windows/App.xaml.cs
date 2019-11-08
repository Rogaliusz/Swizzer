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
using Swizzer.Client.ViewModels;
using Swizzer.Client.Services;
using Swizzer.Client.Validators;
using Swizzer.Client.Cqrs;
using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Web.Api;
using Swizzer.Client.Domain.Users;
using Swizzer.Client.Mapper;
using AutoMapper;

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
            AppDomain.CurrentDomain.UnhandledException += HandleUnexpectedException;

            containerRegistry.Register<INavigationService, NavigationService>();

            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<ChatView>();

            containerRegistry.Register<RegisterViewModelValidator>();
            containerRegistry.Register<IViewModelFacade, ViewModelFacade>();

            containerRegistry.Register<ICommandDispatcher, CommandDispatcher>();
            containerRegistry.Register<IQueryDispatcher, QueryDispatcher>();
            containerRegistry.Register<IApiHubWebServiceFacade, ApiHubWebServiceFacade>();

            RegisterInterfaces(typeof(ICommandHandler), containerRegistry);
            RegisterInterfaces(typeof(IQueryHandler), containerRegistry);

            containerRegistry.RegisterSingleton<IApiHttpWebService, ApiHttpWebService>();
            containerRegistry.RegisterSingleton<MessageApiHubWebService>();
            containerRegistry.RegisterSingleton<ApiSettings>();
            containerRegistry.RegisterSingleton<ICurrentUserContext, CurrentUserContext>();

            var mapper = SwizzerMapperConfiguration.Initialize();
            containerRegistry.RegisterInstance<IMapper>(mapper);

            containerRegistry.RegisterSingleton<ISwizzerMapper, SwizzerMapper>();

            containerRegistry.RegisterInstance<IContainerProvider>(Container);
        }

        private void HandleUnexpectedException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e);
        }

        private void RegisterInterfaces(Type type, IContainerRegistry containerRegistry)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => type.IsAssignableFrom(x));

            foreach (var impType in types)
            {
                var interfaces = impType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    containerRegistry.Register(@interface, impType);
                }
            }

        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            NavigationService.RegisterViewModel<LoginViewModel, LoginView>();
            NavigationService.RegisterViewModel<RegisterViewModel, RegisterView>();
            NavigationService.RegisterViewModel<ChatViewModel, ChatView>();
            NavigationService.RegisterViewModel<MainViewModel, MainWindow>();
        }
    }
}
