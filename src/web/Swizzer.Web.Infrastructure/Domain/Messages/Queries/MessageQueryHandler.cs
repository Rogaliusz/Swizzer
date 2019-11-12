using Microsoft.EntityFrameworkCore;
using NPag.Extensions;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using Swizzer.Shared.Common.Domain.Messages.Queries;
using Swizzer.Shared.Common.Dto;
using Swizzer.Web.Infrastructure.Cqrs.Queries;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Queries
{
    public class MessageQueryHandler : IQueryHandler<GetMessagesQuery, PaginationDto<MessageDto>>
    {
        private readonly ISwizzerMapper _swizzerMapper;
        private readonly SwizzerContext _context;

        public MessageQueryHandler(
            ISwizzerMapper swizzerMapper,
            SwizzerContext context)
        {
            this._swizzerMapper = swizzerMapper;
            this._context = context;
        }

        public async Task<PaginationDto<MessageDto>> HandleAsync(GetMessagesQuery query)
        {
            var ids = (new[] { query.RequestBy, query.Reciever }).OrderBy(x => x).ToList();
            var querable = _context.Messages.Where(x => ids.Any(s => s == x.ReceiverId) && ids.Any(s => s == x.RecipientId));
            var count = await querable.Where(query).CountAsync();
            var data = await querable.FilterBy(query).ToListAsync();
            var wrapper = new PaginationDto<Message> { Data = data, TotalCount = count };

            return _swizzerMapper.MapTo<PaginationDto<MessageDto>>(wrapper);
        }

        private int PaginationDto<T>(PaginationDto<Message> wrapper)
        {
            throw new NotImplementedException();
        }
    }
}
