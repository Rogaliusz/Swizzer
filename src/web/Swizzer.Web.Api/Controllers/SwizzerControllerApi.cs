using Microsoft.AspNetCore.Mvc;
using Swizzer.Shared.Common.Exceptions;
using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using Swizzer.Web.Infrastructure.Cqrs.Queries;
using Swizzer.Web.Infrastructure.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQueryProvider = Swizzer.Shared.Common.Providers.IQueryProvider;

namespace Swizzer.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class SwizzerControllerApi : Controller
    {
        private readonly ICacheService _cacheService;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public Guid UserId => User?.Identity?.IsAuthenticated == true
            ? Guid.Parse(User.Identity.Name)
            : Guid.Empty;

        public SwizzerControllerApi(
            ICacheService cacheService,
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            this._cacheService = cacheService;
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            Authorize(command);

            await _commandDispatcher.DispatchAsync(command);
        }

        protected async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQueryProvider
        {
            Authorize(query);

            return await _queryDispatcher.DispatchAsync<TQuery, TResult>(query);
        }

        private void Authorize<TCommand>(TCommand command)
        {
            if (command is IAuthenticatedRequestProvider authenticatedRequestProvider)
            {
                authenticatedRequestProvider.RequestBy = UserId;

                if (authenticatedRequestProvider.RequestBy == Guid.Empty)
                {
                    throw new SwizzerServerException(ServerErrorCodes.UnauthorizedAccess);
                }
            }
        }

        protected TEntity GetCachedObject<TEntity>(Guid id)
            => _cacheService.Get<TEntity>(id);
    }
}
