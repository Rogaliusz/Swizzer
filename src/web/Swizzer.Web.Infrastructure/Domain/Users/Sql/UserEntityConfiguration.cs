using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Users.Sql
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
