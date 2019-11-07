using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Swizzer.Shared.Common.Exceptions
{
    public class SwizzerClientException : Exception
    {
        public string ErrorCode { get; }

        public SwizzerClientException(string errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public SwizzerClientException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public SwizzerClientException(string errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected SwizzerClientException(string errorCode, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = errorCode;
        }
    }
}
