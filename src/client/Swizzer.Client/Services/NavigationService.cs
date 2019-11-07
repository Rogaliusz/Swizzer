using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Services
{
    public interface INavigationService
    {
        Task GoToAsync<TViewModel>(object parameter = null);
        Task GoToAsync(string url, object parameter = null);
        Task BackAsync(object parameter = null);
    }
}
