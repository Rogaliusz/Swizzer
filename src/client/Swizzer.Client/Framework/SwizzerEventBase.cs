using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Framework
{
    public class SwizzerEventBase : PubSubEvent
    {
    }

    public class SwizzerEventBase<TMessage> : PubSubEvent<TMessage>
    {
    }
}
