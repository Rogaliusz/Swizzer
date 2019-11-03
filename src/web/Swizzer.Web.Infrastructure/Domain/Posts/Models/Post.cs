using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Posts.Models
{
    public class Post : IIdProvider,
        INameProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AuthorId { get; set; }

        public User Author { get; set; }

        public ICollection<File> Files { get; set; } = new HashSet<File>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
