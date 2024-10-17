using Microsoft.EntityFrameworkCore;
using tickets.Entities;

using Route = tickets.Entities.Route;

namespace tickets.Data
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) :
            base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<RouteStop> RouteStops { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<RouteStop>()
                .HasKey(rs => new { rs.RouteId, rs.StopId });

            modelBuilder
                .Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.RouteStops)
                .HasForeignKey(rs => rs.RouteId);

            modelBuilder
                .Entity<RouteStop>()
                .HasOne(rs => rs.Stop)
                .WithMany(s => s.RouteStops)
                .HasForeignKey(rs => rs.StopId);
        }
    }
}
