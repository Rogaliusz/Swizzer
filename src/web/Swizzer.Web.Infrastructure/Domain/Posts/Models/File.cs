using Swizzer.Shared.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Posts.Models
{
    public class File : IIdProvider
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        
        public Post Post { get; set; }
    }
}
