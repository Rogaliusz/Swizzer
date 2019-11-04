using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swizzer.Shared.Common.Providers;
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
using System.Threading;
using System.Threading.Tasks;

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

        public override int SaveChanges()
        {
            BeforeSaveChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        BeforeAdd(changedEntity.Entity);
                        break;

                    case EntityState.Modified:
                        break;
                }
            }
        }

        public override EntityEntry Add(object entity)
        {
            BeforeAdd(entity);

            return base.Add(entity);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeAdd(entity);

            return base.AddAsync(entity, cancellationToken);
        }

        public override void AddRange(params object[] entities)
        {
            foreach (var entity in entities)
            {
                BeforeAdd(entities);
            }

            base.AddRange(entities);
        }

        public override Task AddRangeAsync(params object[] entities)
        {
            foreach (var entity in entities)
            {
                BeforeAdd(entities);
            }

            return base.AddRangeAsync(entities);
        }


        private void BeforeAdd(object entity)
        {
            if (entity is ICreatedAtProvider createdProvider)
            {
                createdProvider.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
