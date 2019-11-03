using Swizzer.Shared.Common.Providers;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Posts.Models
{
    public class Comment : IIdProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public Guid Id { get; set; } 
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }

        public User Author { get; set; }
        public Post Post { get; set; }

    }
}
