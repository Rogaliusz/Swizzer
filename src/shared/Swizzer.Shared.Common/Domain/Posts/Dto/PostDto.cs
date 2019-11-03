using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Domain.Posts.Dto
{
    class PostDto : IIdProvider,
        INameProvider,
        IContentProvider,
        ICreatedAtProvider
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserDto Author { get; set; }
    }
}
