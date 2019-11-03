using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swizzer.Web.Infrastructure.Domain.Posts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Posts
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId);
        }
    }
}
