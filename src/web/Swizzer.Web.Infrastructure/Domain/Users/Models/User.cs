using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Domain.Posts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Users.Models
{
    public class User : IIdProvider,
        ICreatedAtProvider,
        INameProvider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public ICollection<Message> Messages1 { get; set; } = new HashSet<Message>();
        public ICollection<Message> Messages2 { get; set; } = new HashSet<Message>();

    }
}
