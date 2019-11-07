using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Cqrs.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query);
    }

    public class QueryDispatcher : IQueryDispatcher
    {
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
