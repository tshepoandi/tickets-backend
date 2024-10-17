using Microsoft.EntityFrameworkCore;
using tickets.Entities;

namespace tickets.Data
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) :
            base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
