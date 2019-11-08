using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Web.Api;
using Swizzer.Shared.Common;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Domain.Users.Queries;
using Swizzer.Shared.Common.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Domain.Users.Queries
{
    public class UserQueryHandler : IQueryHandler<PaginationDto<UserDto>, GetUsersQuery>
    {
        private readonly IApiHttpWebService _apiHttpWebService;

        public UserQueryHandler(IApiHttpWebService apiHttpWebService)
        {
            this._apiHttpWebService = apiHttpWebService;
        }
        public Task<PaginationDto<UserDto>> HandleAsync(GetUsersQuery query)
        {
            return _apiHttpWebService.SendAsync<PaginationDto<UserDto>>(HttpMethod.Get, Routes.Users.Main);
        }
    }
}
