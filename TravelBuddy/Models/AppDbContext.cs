using Microsoft.EntityFrameworkCore;

namespace TravelBuddy.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Language> Languages { get; set; }
        // Other DbSets...
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guide>()
                .HasMany(g => g.Languages)
                .WithMany(l => l.Guides)
                .UsingEntity(j => j.ToTable("GuideLanguages"));
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.GuideDetails)
                .WithOne(g => g.User)
                .HasForeignKey<Guide>(g => g.UserId);
        }
    }
}