using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
            => string.IsNullOrEmpty(value);
    }
}
