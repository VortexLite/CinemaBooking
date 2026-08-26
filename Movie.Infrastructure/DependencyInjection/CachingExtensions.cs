using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using Movie.Infrastructure.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class CachingExtensions
    {
        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                var connectionString =
                    configuration.GetConnectionString("redis")
                    ?? throw new InvalidOperationException(
                        "Redis connection string not found.");

                return RedisConnectionFactory
                    .CreateAsync(connectionString)
                    .GetAwaiter()
                    .GetResult();
            });

            services.AddSingleton<CacheEntryTracker>();

            services.AddSingleton<MemoryCacheService>();

            services.AddSingleton<RedisCacheService>();

            services.AddSingleton<ICacheService, HybridCacheService>();

            services.AddSingleton<ICacheSerializer, MessagePackCacheSerializer>();

            services.AddSingleton<ICacheLockProvider, CacheLockProvider>();

            services.AddSingleton<ICacheTagStore, MemoryCacheTagStore>();

            services.AddSingleton<ICacheTagStore, RedisCacheTagStore>();

            return services;
        }
    }
}
