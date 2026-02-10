using FeatureFlags.Application.Services;
using FeatureFlags.Domain.Enums;
using FeatureFlags.Domain.Exceptions;
using FeatureFlags.Tests.TestDoubles;
using Xunit;

namespace FeatureFlags.Tests.Services;

public class FeatureMutationServiceTests
{
    [Fact]
    public async Task Cannot_Create_Duplicate_Feature()
    {
        var repo = new InMemoryFeatureRepository();
        var service = new FeatureMutationService(repo, new InMemoryOverrideRepository());

         service.CreateFeature("Niyas", true, null);

    await Assert.ThrowsAsync<ConflictException>(() =>
            service.CreateFeature("Niyas", false, null));
    }

    [Fact]
    public void Can_Add_Override()
    {
        var features = new InMemoryFeatureRepository();
        features.Add(new() { Key = "Niyas", DefaultEnabled = false });

        var overrides = new InMemoryOverrideRepository();

        var service = new FeatureMutationService(features, overrides);

        service.UpsertOverride(
            "Niyas",
            OverrideScope.User,
            "abcd",
            true);

        var stored = overrides.Get("Niyas", OverrideScope.User, "abcd");

        Assert.NotNull(stored);
        Assert.True(stored!.Enabled);
    }

    [Fact]
    public async void Throws_When_Overriding_Non_Existing_Feature()
    {
        var service = new FeatureMutationService(
            new InMemoryFeatureRepository(),
            new InMemoryOverrideRepository());

     await   Assert.ThrowsAsync<NotFoundException>(() =>
            service.UpsertOverride("X", OverrideScope.User, "abcd", true));
    }
}
