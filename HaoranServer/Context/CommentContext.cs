using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class CommentContext: DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options){ }

        // 注意这里的单复数 要和数据库一致
        public DbSet<Comment> comment { get; set; }
    }
}
