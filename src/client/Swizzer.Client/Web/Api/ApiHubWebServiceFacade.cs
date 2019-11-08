using Prism.Events;
using Swizzer.Client.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Web.Api
{
    public interface IApiHubWebServiceFacade
    {
        ApiSettings ApiSettings { get; }
        IEventAggregator EventAggregator { get; }
        ISwizzerMapper Mapper { get; }
    }

    public class ApiHubWebServiceFacade : IApiHubWebServiceFacade
    {
        public ApiSettings ApiSettings { get; }
        public IEventAggregator EventAggregator { get; }
        public ISwizzerMapper Mapper { get; }

        public ApiHubWebServiceFacade(ApiSettings apiSettings, IEventAggregator eventAggregator, ISwizzerMapper mapper)
        {
            ApiSettings = apiSettings;
            EventAggregator = eventAggregator;
            Mapper = mapper;
        }



    }
}
