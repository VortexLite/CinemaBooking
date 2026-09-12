using Movie.Application.Common.Interfaces;
using StackExchange.Redis;

namespace Movie.Infrastructure.Caching
{
    public sealed class RedisCacheTagStore
        : ICacheTagStore
    {
        private readonly IDatabase _database;

        public RedisCacheTagStore(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task AddAsync(string key, 
            IEnumerable<string> tags, 
            CancellationToken cancellationToken = default)
        {
            foreach (var tag in tags)
            {
                await _database.SetAddAsync($"tag:{tag}", key);

                await _database.SetAddAsync($"key:{key}", tag);
            }
        }

        public async Task<IReadOnlyCollection<string>> GetKeysAsync(string tag, 
            CancellationToken cancellationToken = default)
        {
            RedisValue[] values = await _database.SetMembersAsync($"tag:{tag}");

            return values.Select(x => x.ToString()).ToArray();
        }

        public async Task RemoveTagAsync(string tag, 
            CancellationToken cancellationToken = default)
        {
            var keys = await _database.SetMembersAsync($"tag:{tag}");

            foreach (var key in keys)
                await _database.SetRemoveAsync($"key:{key}", tag);

            await _database.KeyDeleteAsync($"tag:{tag}");
        }

        public async Task RemoveKeyAsync(string key,
            CancellationToken cancellationToken = default)
        {
            RedisValue[] tags = await _database.SetMembersAsync($"key:{key}");

            foreach (var tag in tags)
                await _database.SetRemoveAsync($"tag:{tag}", key);

            await _database.KeyDeleteAsync($"key:{key}");
        }
    }
}
