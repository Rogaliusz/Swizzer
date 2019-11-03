using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Posts.Dto
{
    public class CommentDto : IIdProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }

        public UserDto Author { get; set; }
    }
}
