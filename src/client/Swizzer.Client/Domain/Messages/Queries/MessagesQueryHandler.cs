using Swizzer.Client.Cqrs.Queries;
using Swizzer.Client.Web.Api;
using Swizzer.Shared.Common;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Shared.Common.Domain.Messages.Queries;
using Swizzer.Shared.Common.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Domain.Messages.Queries
{
    public class MessagesQueryHandler : IQueryHandler<PaginationDto<MessageDto>, GetMessagesQuery>
    {
        private readonly IApiHttpWebService _apiHttpWebService;
        public MessagesQueryHandler(IApiHttpWebService apiHttpWebService)
        {
            _apiHttpWebService = apiHttpWebService;
        }

        public Task<PaginationDto<MessageDto>> HandleAsync(GetMessagesQuery query)
        {
            return _apiHttpWebService.SendAsync<PaginationDto<MessageDto>>(HttpMethod.Get, Routes.Messages.Main + query.Reciever);
        }
    }
}
