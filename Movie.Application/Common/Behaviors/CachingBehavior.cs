using MediatR;
using Microsoft.Extensions.Logging;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Behaviors
{
    public sealed class CachingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery<TResponse>
    {
        private readonly ICacheService _cache;
        private readonly ICacheLockProvider _cacheLockProvider;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;


        public CachingBehavior(ICacheService cache,
            ICacheLockProvider cacheLockProvider,
            ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _cacheLockProvider = cacheLockProvider;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var cached = await _cache.GetAsync<TResponse>(
                request.CacheKey, 
                cancellationToken);

            if (cached is not null)
            {
                _logger.LogInformation("Cache HIT {CacheKey}",
                    request.CacheKey);

                return cached;
            }

            _logger.LogInformation("Cache MISS {CacheKey}",
                request.CacheKey);

            await using var cacheLock = await _cacheLockProvider.LockAsync(
                request.CacheKey,
                cancellationToken);

            cached = await _cache.GetAsync<TResponse>(
                request.CacheKey,
                cancellationToken);

            if (cached is not null)
            {
                _logger.LogInformation("Cache HIT {CacheKey}",
                    request.CacheKey);

                return cached;
            }

            var response = await next();

            await _cache.SetAsync(
                request.CacheKey,
                response,
                request.Expiration,
                request.Tags,
                cancellationToken);

            _logger.LogInformation("Cache SET {CacheKey}",
                request.CacheKey);

            return response;
        }
    }
}
