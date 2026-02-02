namespace FeatureFlags.Infrastructure.Entities;

public class FeatureFlagEntity
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public bool DefaultEnabled { get; set; }
    public string? Description { get; set; }
}
