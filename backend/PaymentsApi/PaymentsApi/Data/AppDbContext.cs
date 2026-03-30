using Microsoft.EntityFrameworkCore;

namespace PaymentsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasIndex(x => x.ClientRequestId)
                .IsUnique();
        }
    }
}

