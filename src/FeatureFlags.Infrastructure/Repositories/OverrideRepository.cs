using FeatureFlags.Application.Interfaces;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;
using FeatureFlags.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Infrastructure.Repositories
{

    public class OverrideRepository : IOverrideRepository
    {
        private readonly FeatureFlagDbContext _db;

        public OverrideRepository(FeatureFlagDbContext db)
        {
            _db = db;
        }

        public IReadOnlyList<FeatureOverride> GetForFeature(string featureKey)
            => _db.Overrides
                .Where(o => o.FeatureKey == featureKey)
                .Select(o => new FeatureOverride
                {
                    FeatureKey = o.FeatureKey,
                    Scope = o.Scope,
                    TargetId = o.TargetId,
                    Enabled = o.Enabled
                })
                .ToList();

        public FeatureOverride? Get(string featureKey, OverrideScope scope, string targetId)
            => GetForFeature(featureKey)
                .FirstOrDefault(o => o.Scope == scope && o.TargetId == targetId);

        public void Add(FeatureOverride featureOverride)
        {
            _db.Overrides.Add(new()
            {
                FeatureKey = featureOverride.FeatureKey,
                Scope = featureOverride.Scope,
                TargetId = featureOverride.TargetId,
                Enabled = featureOverride.Enabled
            });
            _db.SaveChanges();
        }

        public void Update(FeatureOverride featureOverride)
        {
            var entity = _db.Overrides.First(o =>
                o.FeatureKey == featureOverride.FeatureKey &&
                o.Scope == featureOverride.Scope &&
                o.TargetId == featureOverride.TargetId);

            entity.Enabled = featureOverride.Enabled;
            _db.SaveChanges();
        }

        public void Remove(FeatureOverride featureOverride)
        {
            var entity = _db.Overrides.First(o =>
                o.FeatureKey == featureOverride.FeatureKey &&
                o.Scope == featureOverride.Scope &&
                o.TargetId == featureOverride.TargetId);

            _db.Overrides.Remove(entity);
            _db.SaveChanges();
        }
    }
}