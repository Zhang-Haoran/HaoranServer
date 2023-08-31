using HaoranServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HaoranServer.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options) { }

        public DbSet<User> user { get; set; }
    }
}
