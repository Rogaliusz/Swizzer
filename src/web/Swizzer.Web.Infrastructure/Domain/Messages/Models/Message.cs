using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Models
{
    public class Message : IIdProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ReceiverId { get; set; }
        public Guid RecipientId { get; set; }

        public User Receiver { get; set; }
        public User Recipient { get; set; }
    }
}
