using System.Diagnostics.Metrics;

namespace Movie.Infrastructure.Caching
{
    public static class CacheMetrics
    {
        public const string MeterName = "Movie.Cache";

        public static readonly Meter Meter = new(MeterName);

        // Memory
        public static readonly Counter<long> MemoryHits =
            Meter.CreateCounter<long>("movie.cache.memory.hit");

        public static readonly Counter<long> MemoryMisses =
            Meter.CreateCounter<long>("movie.cache.memory.miss");

        public static readonly Counter<long> MemorySets =
            Meter.CreateCounter<long>("movie.cache.memory.set");

        public static readonly Counter<long> MemoryRemoves =
            Meter.CreateCounter<long>("movie.cache.memory.remove");

        // Redis
        public static readonly Counter<long> RedisHits =
            Meter.CreateCounter<long>("movie.cache.redis.hit");

        public static readonly Counter<long> RedisMisses =
            Meter.CreateCounter<long>("movie.cache.redis.miss");

        public static readonly Counter<long> RedisSets =
            Meter.CreateCounter<long>("movie.cache.redis.set");

        public static readonly Counter<long> RedisRemoves =
            Meter.CreateCounter<long>("movie.cache.redis.remove");
    }
}
