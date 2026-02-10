using FeatureFlags.Application.Services;
using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Exceptions;
using FeatureFlags.Tests.TestDoubles;

namespace FeatureFlags.Tests.Services;

public class FeatureEvaluationServiceTests
{
    [Fact]
    public async Task Throws_When_Feature_Not_Found()
    {
        var service = new FeatureEvaluationService(
            new InMemoryFeatureRepository(),
            new InMemoryOverrideRepository(),
            new FeatureFlagEngine());

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.IsEnabled("Missing", new FeatureContext()));
    }

    [Fact]
    public async Task Returns_Correct_State()
    {
        var features = new InMemoryFeatureRepository();
        features.Add(new FeatureFlag { Key = "Beta", DefaultEnabled = true });

        var service = new FeatureEvaluationService(
            features,
            new InMemoryOverrideRepository(),
            new FeatureFlagEngine());

        var result = await  service.IsEnabled("Beta", new FeatureContext());

        Assert.True(result);
    }
}
