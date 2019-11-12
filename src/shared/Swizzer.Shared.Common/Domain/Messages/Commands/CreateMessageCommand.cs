using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Messages.Commands
{
    public class CreateMessageCommand : IAuthenticatedRequestProvider, ICommandProvider
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid Receiver { get; set; }
        public Guid RequestBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
