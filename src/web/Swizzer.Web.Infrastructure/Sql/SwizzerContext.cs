using Microsoft.EntityFrameworkCore;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Domain.Messages.Sql;
using Swizzer.Web.Infrastructure.Domain.Posts;
using Swizzer.Web.Infrastructure.Domain.Posts.Models;
using Swizzer.Web.Infrastructure.Domain.Posts.Sql;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Domain.Users.Sql;
using Swizzer.Web.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Sql
{
    public class SwizzerContext : DbContext
    {
        private readonly SqlSettings _sqlSettings;

        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        public SwizzerContext(SqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_sqlSettings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwizzerContext).Assembly);

            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
