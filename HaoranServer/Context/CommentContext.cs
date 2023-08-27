using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class CommentContext: DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options){ }

        public DbSet<Comment> comments { get; set; }
    }
}
