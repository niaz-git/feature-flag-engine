using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Domain.Entities
{
    public class FeatureFlag
    {
        public string Key { get; init; } = null!;
        public bool DefaultEnabled { get; set; }
        public string? Description { get; set; }
    }

}
