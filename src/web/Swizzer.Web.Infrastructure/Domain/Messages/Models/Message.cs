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

        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
