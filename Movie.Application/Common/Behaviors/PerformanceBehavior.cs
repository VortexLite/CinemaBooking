using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Movie.Application.Common.Behaviors
{
    public sealed class PerformanceBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            TResponse response = await next();

            stopwatch.Stop();

            long elapsed = stopwatch.ElapsedMilliseconds;

            if (elapsed > 1000)
            {
                _logger.LogWarning(
                    "{RequestName} executed in {Elapsed} ms",
                    typeof(TRequest).Name,
                    elapsed);
            }
            else
            {
                _logger.LogInformation(
                    "{RequestName} executed in {Elapsed} ms",
                    typeof(TRequest).Name,
                    elapsed);
            }

            return response;
        }
    }
}
