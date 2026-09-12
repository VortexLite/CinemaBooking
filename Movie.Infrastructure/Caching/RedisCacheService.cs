using Microsoft.Extensions.Logging;
using Movie.Application.Common.Interfaces;
using StackExchange.Redis;

namespace Movie.Infrastructure.Caching
{
    public sealed class RedisCacheService
    {

        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _redis;
        private readonly ICacheSerializer _serializer;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(
        IConnectionMultiplexer redis,
        ICacheSerializer serializer,
        ILogger<RedisCacheService> logger)
        {
            _redis = redis;

            _database = redis.GetDatabase();

            _serializer = serializer;

            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            RedisValue value = await _database.StringGetAsync(key);

            if (!value.HasValue)
            {
                CacheMetrics.RedisMisses.Add(1);

                _logger.LogDebug("Redis cache MISS {Key}", key);

                return default;
            }

            CacheMetrics.RedisHits.Add(1);

            _logger.LogDebug("Redis cache HIT {Key}", key);

            return _serializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            byte[] bytes = _serializer.Serialize(value);

            await _database.StringSetAsync(key, bytes, expiration);

            CacheMetrics.RedisSets.Add(1);

            _logger.LogDebug("Redis cache SET {Key}", key);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);

            CacheMetrics.RedisRemoves.Add(1);

            _logger.LogDebug("Redis cache REMOVE {Key}", key);
        }

        private async IAsyncEnumerable<RedisKey> ScanAsync(string pattern) 
        {
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);

                await foreach (var key in server.KeysAsync(
                    pattern: pattern))
                {
                    yield return key;
                }
            }
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            var deleted = 0;

            await foreach (var key in ScanAsync($"{prefix}*"))
            {
                if (await _database.KeyDeleteAsync(key))
                    deleted++;
            }

            if (deleted > 0)
            {
                CacheMetrics.RedisRemoves.Add(deleted);

                _logger.LogDebug("Redis cache REMOVE PREFIX {Prefix}. Removed {Count} keys.",
                    prefix, deleted);
            }
        }
    }
}
