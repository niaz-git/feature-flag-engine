using FeatureFlags.Domain.Entities;
using FeatureFlags.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Infrastructure.Persistence
{
    public class FeatureFlagDbContext : DbContext
    {

        public FeatureFlagDbContext(DbContextOptions options) : base(options) { }


        public DbSet<FeatureFlagEntity> Features => Set<FeatureFlagEntity>();
        public DbSet<FeatureOverrideEntity> Overrides => Set<FeatureOverrideEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureFlagEntity>()
                .HasIndex(f => f.Key)
                .IsUnique();

            modelBuilder.Entity<FeatureOverrideEntity>()
                .HasIndex(o => new { o.FeatureKey, o.Scope, o.TargetId })
                .IsUnique();
        }
    }
}
