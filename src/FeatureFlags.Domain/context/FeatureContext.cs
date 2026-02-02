using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Domain.context
{
    public record FeatureContext(
     string? UserId = null,
     string? GroupId = null,
     string? Region = null
 );

}
