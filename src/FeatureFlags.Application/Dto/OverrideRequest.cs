using FeatureFlags.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Application.Dto
{
    public record OverrideRequest(
     OverrideScope Scope,
     string TargetId,
     bool Enabled
 );
}
