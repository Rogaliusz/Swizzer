using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Framework.Security
{
    public class SecuritySettings
    {
        public TimeSpan TokenDuration { get; set; }
        public string SecredKey { get; set; }
    }
}
