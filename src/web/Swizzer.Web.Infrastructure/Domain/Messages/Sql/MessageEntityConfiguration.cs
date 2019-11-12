using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Sql
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.Messages1)
                .HasForeignKey(x => x.ReceiverId);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.Messages2)
                .HasForeignKey(x => x.SenderId);
        }
    }
}
