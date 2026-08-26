using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Caching
{
    public sealed class HybridCacheService : ICacheService
    {
        private readonly MemoryCacheService _memory;
        private readonly RedisCacheService _redis;
        private readonly ICacheTagStore _tagStore;

        public HybridCacheService(MemoryCacheService memory, 
            RedisCacheService redis,
            ICacheTagStore tagStore)
        {
            _memory = memory;
            _redis = redis;
            _tagStore = tagStore;
        }

        public async Task<T?> GetAsync<T>(string key,
            CancellationToken cancellationToken = default)
        {
            if (_memory.TryGet(key, out T? value))
                return value;

            value = await _redis.GetAsync<T>(key);

            if (value is not null)
            {
                _memory.Set(
                    key,
                    value,
                    TimeSpan.FromMinutes(5));
            }

            return value;
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan? expiration = null,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            TimeSpan ttl =
                expiration ?? TimeSpan.FromMinutes(10);

            _memory.Set(
                key,
                value,
                ttl);

            await _redis.SetAsync(
                key,
                value,
                ttl);

            if (tags is not null)
            {
                await _tagStore.AddAsync(
                    key,
                    tags,
                    cancellationToken);
            }
        }

        public async Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            _memory.Remove(key);

            await _redis.RemoveAsync(key);
        }

        public async Task RemoveByPrefixAsync(
            string prefix,
            CancellationToken cancellationToken = default)
        {
            _memory.RemoveByPrefix(prefix);

            await _redis.RemoveByPrefixAsync(prefix);
        }

        public async Task RemoveByTagAsync(string tag,
            CancellationToken cancellationToken = default)
        {
            var keys = await _tagStore.GetKeysAsync(tag, cancellationToken);

            foreach (var key in keys)
            {
                await RemoveAsync(key, cancellationToken);
            }

            await _tagStore.RemoveTagAsync(tag, cancellationToken);
        }
    }
}
