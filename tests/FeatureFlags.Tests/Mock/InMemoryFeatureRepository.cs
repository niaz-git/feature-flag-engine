    using FeatureFlags.Application.Interfaces;
    using FeatureFlags.Domain.Entities;

    namespace FeatureFlags.Tests.TestDoubles;

    public class InMemoryFeatureRepository : IFeatureRepository
    {
        private readonly Dictionary<string, FeatureFlag> _store = new();

        public FeatureFlag? Get(string key)
            => _store.TryGetValue(key, out var feature) ? feature : null;

        public IReadOnlyList<FeatureFlag> GetAll()
            => _store.Values.ToList();

        public void Add(FeatureFlag feature)
            => _store.Add(feature.Key, feature);

        public void Update(FeatureFlag feature)
            => _store[feature.Key] = feature;
    }
