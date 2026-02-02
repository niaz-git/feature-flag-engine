using FeatureFlags.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Domain.Entities
{
    public class FeatureOverride
    {
        public string FeatureKey { get; init; } = null!;
        public OverrideScope Scope { get; init; }
        public string TargetId { get; init; } = null!;
        public bool Enabled { get; set; }
    }

}
