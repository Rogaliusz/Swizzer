using System.Collections.Generic;
using System;
using System.Text;
using Swizzer.Web.Infrastructure.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swizzer.Web.Infrastructure.Domain.Posts.Sql
{
    public class FileEntityConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.PostId);

        }
    }
}
