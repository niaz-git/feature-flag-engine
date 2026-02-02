using FeatureFlags.Application.Interfaces;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;
using FeatureFlags.Domain.Exceptions;

namespace FeatureFlags.Application.Services;

public class FeatureMutationService
{
    private readonly IFeatureRepository _features;
    private readonly IOverrideRepository _overrides;

    public FeatureMutationService(
        IFeatureRepository features,
        IOverrideRepository overrides)
    {
        _features = features;
        _overrides = overrides;
    }

    public void CreateFeature(string key, bool defaultEnabled, string? description)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ValidationException("Feature key is required");

        if (_features.Get(key) != null)
            throw new ConflictException("Feature already exists");

        _features.Add(new FeatureFlag
        {
            Key = key,
            DefaultEnabled = defaultEnabled,
            Description = description
        });
    }

    public void UpsertOverride(
        string featureKey,
        OverrideScope scope,
        string targetId,
        bool enabled)
    {
        if (string.IsNullOrWhiteSpace(targetId))
            throw new ValidationException("TargetId is required");

        var feature = _features.Get(featureKey)
            ?? throw new NotFoundException("Feature not found");

        var existing = _overrides.Get(featureKey, scope, targetId);

        if (existing == null)
        {
            _overrides.Add(new FeatureOverride
            {
                FeatureKey = featureKey,
                Scope = scope,
                TargetId = targetId,
                Enabled = enabled
            });
        }
        else
        {
            existing.Enabled = enabled;
            _overrides.Update(existing);
        }
    }
}
