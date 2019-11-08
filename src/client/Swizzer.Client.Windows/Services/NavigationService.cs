using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Swizzer.Client.Services;
using Swizzer.Client.ViewModels;
using Swizzer.Client.Windows.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Swizzer.Client.Windows
{
    public class NavigationService : INavigationService
    {
        private readonly static IDictionary<Type, Type> _bindedViews = new Dictionary<Type, Type>();
        private readonly IRegionNavigationService _navigationService;
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;
        private readonly Stack<Type> _views = new Stack<Type>();

        private Type _currentView;

        public NavigationService(
            IContainerExtension container,
            IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;

            _currentView = typeof(LoginView);
        }

        public async Task BackAsync(object parameter = null)
        {
            var viewModel = _views.Pop();
            await GoToAsync(viewModel, parameter);
        }

        public Task GoToAsync<TViewModel>(object parameter = null)
            => GoToAsync(typeof(TViewModel), parameter);

        public async Task GoToAsync(Type viewModelType, object parameter = null)
        {
            var viewType = _bindedViews[viewModelType];

            _views.Push(_currentView);
            _currentView = viewType;

            var parameters = new NavigationParameters
            {
                { "value", parameter }
            };

            if (_regionManager.Regions["ContentRegion"].Views.Any(x => x.GetType() != viewType))
            {
                var view = _container.Resolve(viewType);
                _regionManager.Regions["ContentRegion"].Add(view);
            }

            _regionManager.RequestNavigate("ContentRegion", viewType.Name, x => navigationCallback(x, viewType), parameters);
        }

        private void navigationCallback(NavigationResult obj, Type viewType)
        {
            var viewInstance = obj.Context.NavigationService.Region.ActiveViews.FirstOrDefault(x => x.GetType() == viewType);
            var dataContext = viewInstance.GetType().GetProperty(nameof(ContentControl.DataContext)).GetValue(viewInstance);
            var viewModelBase = dataContext as ViewModelBase;

            if (viewModelBase == null)
            {
                return;
            }

            obj.Context.Parameters.TryGetValue<object>("value", out var parameter);

            viewModelBase.InitializeAsync(parameter);
        }

        public Task GoToAsync(string url, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public static void RegisterViewModel<TViewModel, TView>()
        {
            _bindedViews.Add(typeof(TViewModel), typeof(TView));
            ViewModelLocationProvider.Register<TView, TViewModel>();
        }
    }
}
