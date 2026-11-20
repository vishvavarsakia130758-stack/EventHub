using Microsoft.EntityFrameworkCore;
using EventHub.Models;

namespace EventHub.Data
{
    // SQLite database (a single file: App_Data/eventhub.db). Created automatically on first run.
    public class AppDbContext : DbContext
    {
        public static string ConnectionString = "Data Source=eventhub.db";

        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            // NOCASE makes email comparisons case-insensitive, like the old in-memory code.
            b.Entity<User>().Property(u => u.Email).UseCollation("NOCASE");
            b.Entity<User>().HasIndex(u => u.Email).IsUnique();

            b.Entity<Registration>().Property(r => r.Email).UseCollation("NOCASE");
            b.Entity<Registration>().HasIndex(r => r.Code).IsUnique();
            b.Entity<Registration>().HasIndex(r => new { r.EventId, r.Email });
        }
    }
}
