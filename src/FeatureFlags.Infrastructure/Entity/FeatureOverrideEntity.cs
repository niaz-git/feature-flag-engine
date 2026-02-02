using FeatureFlags.Domain.Enums;

namespace FeatureFlags.Infrastructure.Entities;

public class FeatureOverrideEntity
{
    public int Id { get; set; }
    public string FeatureKey { get; set; } = null!;
    public OverrideScope Scope { get; set; }
    public string TargetId { get; set; } = null!;
    public bool Enabled { get; set; }
}
