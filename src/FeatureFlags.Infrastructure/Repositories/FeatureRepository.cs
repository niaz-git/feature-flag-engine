using FeatureFlags.Application.Interfaces;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Infrastructure.Persistence;


namespace FeatureFlags.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly FeatureFlagDbContext _db;

        public FeatureRepository(FeatureFlagDbContext db)
        {
            _db = db;
        }

        public FeatureFlag? Get(string key)
            => _db.Features
                .Where(f => f.Key == key)
                .Select(f => new FeatureFlag
                {
                    Key = f.Key,
                    DefaultEnabled = f.DefaultEnabled,
                    Description = f.Description
                })
                .FirstOrDefault();

        public IReadOnlyList<FeatureFlag> GetAll()
            => _db.Features
                .Select(f => new FeatureFlag
                {
                    Key = f.Key,
                    DefaultEnabled = f.DefaultEnabled,
                    Description = f.Description
                })
                .ToList();

        public void Add(FeatureFlag feature)
        {
            _db.Features.Add(new()
            {
                Key = feature.Key,
                DefaultEnabled = feature.DefaultEnabled,
                Description = feature.Description
            });
            _db.SaveChanges();
        }

        public void Update(FeatureFlag feature)
        {
            var entity = _db.Features.First(f => f.Key == feature.Key);
            entity.DefaultEnabled = feature.DefaultEnabled;
            _db.SaveChanges();
        }
    }
}
