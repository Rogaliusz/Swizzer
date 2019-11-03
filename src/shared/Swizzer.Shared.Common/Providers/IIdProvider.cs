using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Providers
{
    public interface IIdProvider
    {
        Guid Id { get; set; }
    }
}
