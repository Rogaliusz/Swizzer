using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swizzer.Shared.Common;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Shared.Common.Domain.Messages.Queries;
using Swizzer.Shared.Common.Dto;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using Swizzer.Web.Infrastructure.Cqrs.Queries;
using Swizzer.Web.Infrastructure.Framework;

namespace Swizzer.Web.Api.Controllers
{
    public class MessagesController : SwizzerControllerApi
    {
        public MessagesController(ICacheService cacheService, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : base(cacheService, queryDispatcher, commandDispatcher)
        {
        }

        [HttpGet("{recieverId}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(Guid recieverId, [FromQuery] GetMessagesQuery query )
        {
            query.Reciever = recieverId;
            var results = await DispatchQueryAsync<GetMessagesQuery, PaginationDto<MessageDto>>(query);

            return Ok(results);
        }
    }
}
