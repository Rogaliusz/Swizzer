using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Exceptions
{
    public class ErrorDto
    {
        public Exception Exception { get; set; }
        public string ErrorCode { get; set; }

    }
}
