using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Domain.Engine
{
    public interface IFeatureFlagEngine
    {
        bool IsEnabled(
            FeatureFlag feature,
            IEnumerable<FeatureOverride> overrides,
            FeatureContext context);
    }
}
