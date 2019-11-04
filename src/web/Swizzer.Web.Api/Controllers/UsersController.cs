using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swizzer.Shared.Common.Domain.Users.Commands;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Domain.Users.Queries;
using Swizzer.Shared.Common.Dto;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using Swizzer.Web.Infrastructure.Cqrs.Queries;
using Swizzer.Web.Infrastructure.Framework;

namespace Swizzer.Web.Api.Controllers
{

    public class UsersController : SwizzerControllerApi
    {
        public UsersController(ICacheService cacheService, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : base(cacheService, queryDispatcher, commandDispatcher)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginCommand command)
        {
            await DispatchCommandAsync(command);
            var jwt = GetCachedObject<JwtDto>(command.Id);
            return Ok(jwt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserRegisterCommand command)
        {
            await DispatchCommandAsync(command);
            var user = GetCachedObject<UserDto>(command.Id);
            return Created($"api/users/{command.Id}", user);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var query = new GetUserQuery { Id = id };
            var user = await DispatchQueryAsync<GetUserQuery, UserDto>(query);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersQuery query)
        {
            var users = await DispatchQueryAsync<GetUsersQuery, PaginationDto<UserDto>>(query);
            return Ok(users);
        }

    }
}
