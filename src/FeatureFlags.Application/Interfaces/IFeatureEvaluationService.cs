using FeatureFlags.Domain.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Application.Interfaces
{
    public interface IFeatureEvaluationService
    {
        bool IsEnabled(string featureKey, FeatureContext context);
    }

}
