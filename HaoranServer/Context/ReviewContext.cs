using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class ReviewContext: DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }

        public DbSet<Review> review { get; set; }
    }
}
