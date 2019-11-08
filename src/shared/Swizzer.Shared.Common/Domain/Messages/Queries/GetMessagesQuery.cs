using Swizzer.Shared.Common.Cqrs;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Messages.Queries
{
    public class GetMessagesQuery : SwizzerPaginationQueryBase, IQueryProvider, IAuthenticatedRequestProvider
    {
        public Guid Reciever { get; set; }
        public Guid RequestBy { get; set; }
    }
}
