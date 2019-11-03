using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Users.Queries
{
    public class GetUserQuery : IAuthenticatedRequestProvider, 
        IQueryProvider
    {
        public Guid Id { get; set; }
        public Guid RequestBy { get; set; }
    }
}
