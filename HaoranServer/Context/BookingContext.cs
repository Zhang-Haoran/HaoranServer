using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class BookingContext: DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<Booking> booking { get; set; }
    }
}
