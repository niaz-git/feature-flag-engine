using FeatureFlags.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Application.Interfaces
{
    public interface IFeatureMutationService
    {
        Task CreateFeature(string key, bool defaultEnabled, string? description);
        Task UpsertOverride(string featureKey, OverrideScope scope, string targetId, bool enabled);
    }
}
