using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class CommentContext: DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options){ }

        // 注意这里的单复数 要和数据库一致
        public DbSet<Comment> comment { get; set; }

        // 这段神奇的代码复写了saveChangesAsync function 在CommentContext中，使得createdTime在修改时 不会被更新
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Comment && (e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                // Ensure CreatedTime is not modified
                entityEntry.Property("CreatedTime").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        }
}
