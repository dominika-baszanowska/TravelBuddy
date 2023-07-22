using Microsoft.EntityFrameworkCore;

namespace TravelBuddy.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.GuideDetails)
                .WithOne(g => g.User)
                .HasForeignKey<Guide>(g => g.UserId);

            modelBuilder.Entity<Guide>()
                .HasMany(g => g.Languages)
                .WithMany(l => l.Guides)
                .UsingEntity(j => j.ToTable("GuideLanguages"));

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Guide)
                .WithMany(g => g.Trips)
                .HasForeignKey(t => t.GuideId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Country)
                .WithMany(g => g.Trips)
                .HasForeignKey(t => t.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.City)
                .WithMany(g => g.Trips)
                .HasForeignKey(t => t.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}