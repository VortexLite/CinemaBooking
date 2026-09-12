using StackExchange.Redis;

namespace Movie.Infrastructure.Caching
{
    internal class RedisConnectionFactory
    {
        public static async Task<IConnectionMultiplexer> CreateAsync(
        string connectionString)
        {
            return await ConnectionMultiplexer.ConnectAsync(
                connectionString);
        }
    }
}
