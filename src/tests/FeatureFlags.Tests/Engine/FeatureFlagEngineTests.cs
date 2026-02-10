using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Engine;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;
using Xunit;

namespace FeatureFlags.Tests.Engine;

public class FeatureFlagEngineTests
{
    private readonly FeatureFlagEngine _engine = new();

    [Fact]
    public void Uses_Global_Default_When_No_Overrides()
    {
        var feature = new FeatureFlag
        {
            Key = "NewUI",
            DefaultEnabled = true
        };

        var result = _engine.IsEnabled(
            feature,
            [],
            new FeatureContext());

        Assert.True(result);
    }

    [Fact]
    public void User_Override_Takes_Priority()
    {
        var feature = new FeatureFlag { Key = "NewUI", DefaultEnabled = false };

        var overrides = new[]
        {
            new FeatureOverride
            {
                FeatureKey = "NewUI",
                Scope = OverrideScope.User,
                TargetId = "u1",
                Enabled = true
            }
        };

        var result = _engine.IsEnabled(
            feature,
            overrides,
            new FeatureContext(UserId: "u1"));

        Assert.True(result);
    }

    [Fact]
    public void Group_Override_Used_When_No_User_Override()
    {
        var feature = new FeatureFlag { Key = "NewUI", DefaultEnabled = false };

        var overrides = new[]
        {
            new FeatureOverride
            {
                FeatureKey = "NewUI",
                Scope = OverrideScope.Group,
                TargetId = "g1",
                Enabled = true
            }
        };

        var result = _engine.IsEnabled(
            feature,
            overrides,
            new FeatureContext(GroupId: "g1"));

        Assert.True(result);
    }

    [Fact]
    public void Region_Override_Used_When_No_User_Or_Group()
    {
        var feature = new FeatureFlag { Key = "NewUI", DefaultEnabled = false };

        var overrides = new[]
        {
            new FeatureOverride
            {
                FeatureKey = "NewUI",
                Scope = OverrideScope.Region,
                TargetId = "IN",
                Enabled = true
            }
        };

        var result = _engine.IsEnabled(
            feature,
            overrides,
            new FeatureContext(Region: "IN"));

        Assert.True(result);
    }
}
