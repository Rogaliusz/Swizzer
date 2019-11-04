using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Hubs
{
    public static class Channels
    {
        public const string ChatChannel = "chat";

        public static class Chat
        {
            public const string Messages = "SendMessageAsync";
        }
    }
}
