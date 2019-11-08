using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Cqrs.Queries
{
    public interface IQueryHandler
    {

    }

    public interface IQueryHandler<TResult, TQuery> : IQueryHandler
        where TQuery : IQueryProvider
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
