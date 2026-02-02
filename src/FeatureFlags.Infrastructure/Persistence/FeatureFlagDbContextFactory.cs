using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace FeatureFlags.Infrastructure.Persistence
{
  

public class FeatureFlagDbContextFactory
       : IDesignTimeDbContextFactory<FeatureFlagDbContext>
    {
        public FeatureFlagDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<FeatureFlagDbContext>();

            var dbPath = Path.Combine(
           Directory.GetCurrentDirectory(),
           "featureflags.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");


            // optionsBuilder.UseSqlServer(  
            //     "Server=.;Database=FeatureFlagsDb;Trusted_Connection=True;");  

            return new FeatureFlagDbContext(optionsBuilder.Options);
        }
    }

}
