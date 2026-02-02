using FeatureFlags.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureFlags.Application.Interfaces
{

    public interface IFeatureRepository
    {
        FeatureFlag? Get(string key);
        IReadOnlyList<FeatureFlag> GetAll();
        void Add(FeatureFlag feature);
        void Update(FeatureFlag feature);
    }
}
