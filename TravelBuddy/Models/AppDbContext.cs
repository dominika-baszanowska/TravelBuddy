﻿using Microsoft.EntityFrameworkCore;

namespace TravelBuddy.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        // Other DbSets...
    }
}