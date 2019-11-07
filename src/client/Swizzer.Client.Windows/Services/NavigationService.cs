using Prism.Mvvm;
using Prism.Regions;
using Swizzer.Client.Services;
using Swizzer.Client.Windows.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Windows
{
    public class NavigationService : INavigationService
    {
        private readonly static IDictionary<Type, Type> _bindedViews = new Dictionary<Type, Type>();

        private readonly IRegionManager _regionManager;
        private readonly Stack<Type> _views = new Stack<Type>();

        private Type _currentView;

        public NavigationService(IRegionManager regionManager)
        {
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

            _regionManager.RequestNavigate("ContentRegion", viewType.Name, parameters);
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
