using FeatureFlags.Application.Services;
using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Exceptions;
using FeatureFlags.Tests.TestDoubles;
using Xunit;

namespace FeatureFlags.Tests.Services;

public class FeatureEvaluationServiceTests
{
    [Fact]
    public void Throws_When_Feature_Not_Found()
    {
        var service = new FeatureEvaluationService(
            new InMemoryFeatureRepository(),
            new InMemoryOverrideRepository(),
            new FeatureFlagEngine());

        Assert.Throws<NotFoundException>(() =>
            service.IsEnabled("Missing", new FeatureContext()));
    }

    [Fact]
    public void Returns_Correct_State()
    {
        var features = new InMemoryFeatureRepository();
        features.Add(new FeatureFlag { Key = "Beta", DefaultEnabled = true });

        var service = new FeatureEvaluationService(
            features,
            new InMemoryOverrideRepository(),
            new FeatureFlagEngine());

        var result = service.IsEnabled("Beta", new FeatureContext());

        Assert.True(result);
    }
}
