using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Cqrs.Queries
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQueryProvider
    {
        Task<TResult> HandleAsync(TQuery query);
    }

}
