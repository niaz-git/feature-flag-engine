using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Domain.Engine
{

    public sealed class FeatureFlagEngine : IFeatureFlagEngine
    {
        public bool IsEnabled(
            FeatureFlag feature,
            IEnumerable<FeatureOverride> overrides,
            FeatureContext context)
        {
            if (context.UserId != null)
            {
                var user = overrides.FirstOrDefault(o =>
                    o.Scope == OverrideScope.User &&
                    o.TargetId == context.UserId);

                if (user != null) return user.Enabled;
            }

            if (context.GroupId != null)
            {
                var group = overrides.FirstOrDefault(o =>
                    o.Scope == OverrideScope.Group &&
                    o.TargetId == context.GroupId);

                if (group != null) return group.Enabled;
            }

            if (context.Region != null)
            {
                var region = overrides.FirstOrDefault(o =>
                    o.Scope == OverrideScope.Region &&
                    o.TargetId == context.Region);

                if (region != null) return region.Enabled;
            }

            return feature.DefaultEnabled;
        }
    }


}
