using Prism.Ioc;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Cqrs.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider;
    }

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IContainerProvider _containerExtension;

        public QueryDispatcher(IContainerProvider containerExtension)
        {
            this._containerExtension = containerExtension;
        }
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider
        {
            var handler = _containerExtension.Resolve<IQueryHandler<TResult, TQuery>>();
            return handler.HandleAsync(query);
        }
    }
}
