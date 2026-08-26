using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxCleanupBackgroundService
        : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromHours(6);
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxCleanupBackgroundService> _logger;

        public OutboxCleanupBackgroundService(
            IServiceScopeFactory scopeFactory, 
            ILogger<OutboxCleanupBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await using var scope = _scopeFactory.CreateAsyncScope();

                    var cleanupService = scope.ServiceProvider
                        .GetRequiredService<IOutboxCleanupService>();

                    await cleanupService.CleanupAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Outbox cleanup failed.");
                }

                await Task.Delay(Interval, stoppingToken);
            }
        }
    }
}
