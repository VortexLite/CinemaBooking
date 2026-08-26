using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Caching
{
    public sealed class MemoryCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly CacheEntryTracker _tracker;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(IMemoryCache cache, 
            CacheEntryTracker tracker,
            ILogger<MemoryCacheService> logger)
        {
            _cache = cache;
            _tracker = tracker;
            _logger = logger;
        }

        public bool TryGet<T>(
            string key,
            out T? value)
        {
            var found = _cache.TryGetValue(key, out value);

            if (found)
            {
                CacheMetrics.MemoryHits.Add(1);

                _logger.LogDebug(
                    "Memory cache HIT {Key}",
                    key);
            }
            else
            {
                CacheMetrics.MemoryMisses.Add(1);

                _logger.LogDebug(
                    "Memory cache MISS {Key}",
                    key);
            }

            return found;
        }

        public void Set<T>(
            string key,
            T value,
            TimeSpan expiration)
        {
            var optins = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            optins.RegisterPostEvictionCallback((k, _, _, _) =>
            {
                _tracker.Remove(k.ToString()!);
            });
            
            _cache.Set(
                key,
                value,
                optins);

            _tracker.Add(key);

            CacheMetrics.MemorySets.Add(1);

            _logger.LogDebug(
                "Memory cache SET {Key}",
                key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);

            _tracker.Remove(key);

            CacheMetrics.MemoryRemoves.Add(1);

            _logger.LogDebug(
                "Memory cache REMOVE {Key}",
                key);
        }

        public void RemoveByPrefix(string prefix)
        {
            var keys = _tracker
                .GetKeys()
                .Where(x => x.StartsWith(prefix))
                .ToList();

            foreach (var key in keys)
            {
                Remove(key);
            }
        }
    }
}
