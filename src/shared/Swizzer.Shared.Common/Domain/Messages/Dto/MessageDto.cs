using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Messages.Dto
{
    public class MessageDto : IIdProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ReceiverId { get; set; }
        public Guid RecipientId { get; set; }

        public UserDto Receiver { get; set; }
        public UserDto Recipient { get; set; }
    }
}
