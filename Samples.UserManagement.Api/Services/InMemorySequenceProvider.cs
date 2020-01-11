using System;
using System.Collections.Concurrent;

namespace Samples.UserManagement.Api.Services
{
    public class InMemorySequenceProvider : ISequenceProvider
    {
        private readonly ConcurrentDictionary<string, int> _sequences = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public static InMemorySequenceProvider Create()
            => new InMemorySequenceProvider();

        public int Increment(string key, int amount = 1)
            => _sequences.AddOrUpdate(key, amount, (k, x) => x + amount);

        public void Reset(string key, int startingAt = 0)
            => _sequences.AddOrUpdate(key, startingAt, (k, x) => startingAt);
    }
}
