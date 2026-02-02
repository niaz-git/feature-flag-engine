using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Application.Dto
{
    public record CreateFeatureRequest(
     string Key,
     bool DefaultEnabled,
     string? Description
 );
}
