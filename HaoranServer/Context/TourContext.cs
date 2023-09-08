using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class TourContext: DbContext
    {
        public TourContext(DbContextOptions<TourContext> options) : base(options) { }

        public DbSet<Tour> tour { get; set; }
    }
}
