using FeatureFlags.Application.Interfaces;
using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Domain.Exceptions;

namespace FeatureFlags.Application.Services;

public class FeatureEvaluationService  : IFeatureEvaluationService
{
    private readonly IFeatureRepository _features;
    private readonly IOverrideRepository _overrides;
    private readonly IFeatureFlagEngine _engine;

    public FeatureEvaluationService(
        IFeatureRepository features,
        IOverrideRepository overrides,
        IFeatureFlagEngine engine)
    {
        _features = features;
        _overrides = overrides;
        _engine = engine;
    }

    public async Task<bool> IsEnabled(string featureKey, FeatureContext context)
    {
        var feature = _features.Get(featureKey)
            ?? throw new NotFoundException("Feature not found");

        var overrides = _overrides.GetForFeature(featureKey);

        return _engine.IsEnabled(feature, overrides, context);
    }
}
