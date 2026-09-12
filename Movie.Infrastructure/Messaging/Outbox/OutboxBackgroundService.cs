using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxBackgroundService
        : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxBackgroundService> _logger;

        public OutboxBackgroundService(IServiceScopeFactory scopeFactory, 
            ILogger<OutboxBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Outbox background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await using var scope = _scopeFactory.CreateAsyncScope();

                    var processor = scope.ServiceProvider
                        .GetRequiredService<IOutboxProcessor>();

                    _logger.LogDebug("Running Outbox processor.");

                    await processor.ProcessAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Outbox background service failed.");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
