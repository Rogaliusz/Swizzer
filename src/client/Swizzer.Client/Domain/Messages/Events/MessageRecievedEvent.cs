using Swizzer.Client.Framework;
using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Shared.Common.Domain.Messages.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Domain.Messages.Events
{
    public class MessageRecievedEvent : SwizzerEventBase<MessageDto>
    {
    }
}
