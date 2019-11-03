using Autofac;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Cqrs.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider;
    }

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _componentContext;

        public QueryDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider
        {
            var handler = _componentContext.Resolve<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}
