using Microsoft.EntityFrameworkCore;
using NPag.Extensions;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Domain.Users.Queries;
using Swizzer.Shared.Common.Dto;
using Swizzer.Web.Infrastructure.Cqrs.Queries;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Domain.Users.Queries
{
    public class UsersQueryHandler : IQueryHandler<GetUserQuery, UserDto>,
        IQueryHandler<GetUsersQuery, PaginationDto<UserDto>>
    {
        private readonly ISwizzerMapper _mapper;
        private readonly SwizzerContext _context;

        public UsersQueryHandler(
            ISwizzerMapper mapper,
            SwizzerContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<PaginationDto<UserDto>> HandleAsync(GetUsersQuery query)
        {
            var count = await _context.Users.Where(query).CountAsync();
            var users = await _context.Users.FilterBy(query).ToListAsync();
            var wrapper = new PaginationDto<User> { Data = users, TotalCount = count };

            return _mapper.MapTo<PaginationDto<UserDto>>(wrapper);
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var user = await _context.Users.FindAsync(query.Id);
            var dto = _mapper.MapTo<UserDto>(user);

            return dto;
        }
    }
}
