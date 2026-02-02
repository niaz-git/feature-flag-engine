using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;

namespace FeatureFlags.Application.Interfaces;

public interface IOverrideRepository
{
    IReadOnlyList<FeatureOverride> GetForFeature(string featureKey);

    FeatureOverride? Get(
        string featureKey,
        OverrideScope scope,
        string targetId);

    void Add(FeatureOverride featureOverride);
    void Update(FeatureOverride featureOverride);
    void Remove(FeatureOverride featureOverride);
}
