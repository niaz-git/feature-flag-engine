using FeatureFlags.Application.Interfaces;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;

namespace FeatureFlags.Tests.TestDoubles;

public class InMemoryOverrideRepository : IOverrideRepository
{
    private readonly List<FeatureOverride> _store = [];

    public IReadOnlyList<FeatureOverride> GetForFeature(string featureKey)
        => _store.Where(o => o.FeatureKey == featureKey).ToList();

    public FeatureOverride? Get(string featureKey, OverrideScope scope, string targetId)
        => _store.FirstOrDefault(o =>
            o.FeatureKey == featureKey &&
            o.Scope == scope &&
            o.TargetId == targetId);

    public void Add(FeatureOverride featureOverride)
        => _store.Add(featureOverride);

    public void Update(FeatureOverride featureOverride)
    {
        Remove(featureOverride);
        Add(featureOverride);
    }

    public void Remove(FeatureOverride featureOverride)
        => _store.RemoveAll(o =>
            o.FeatureKey == featureOverride.FeatureKey &&
            o.Scope == featureOverride.Scope &&
            o.TargetId == featureOverride.TargetId);
}
