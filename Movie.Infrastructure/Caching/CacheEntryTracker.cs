using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Caching
{
    public sealed class CacheEntryTracker
    {
        private readonly ConcurrentDictionary<string, byte> _keys = new();

        public void Add(string key)
        {
            _keys.TryAdd(key, 0);
        }

        public void Remove(string key)
        {
            _keys.TryRemove(key, out _);
        }

        public IReadOnlyCollection<string> GetKeys()
        {
            return _keys.Keys.ToList();
        }
    }
}
