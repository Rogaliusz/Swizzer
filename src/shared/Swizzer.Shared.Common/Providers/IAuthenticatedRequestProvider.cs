using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Providers
{
    public interface IAuthenticatedRequestProvider
    {
        Guid RequestBy { get; set; }
    }
}
