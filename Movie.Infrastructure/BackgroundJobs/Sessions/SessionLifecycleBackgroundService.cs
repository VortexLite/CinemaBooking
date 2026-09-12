using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movie.Application.Common.Interfaces;

namespace Movie.Infrastructure.BackgroundJobs.Sessions
{
    public sealed class SessionLifecycleBackgroundService
        : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly SessionLifecycleOptions _options;
        private readonly ILogger<SessionLifecycleBackgroundService> _logger;

        public SessionLifecycleBackgroundService(IServiceScopeFactory scopeFactory, 
            IOptions<SessionLifecycleOptions> options,
            ILogger<SessionLifecycleBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = TimeSpan.FromSeconds(_options.IntervalSeconds);

            using var timer = new PeriodicTimer(interval);

            _logger.LogInformation("Session lifecycle background service started");

            try
            {
                await ProcessAsync(stoppingToken);

                while (await timer.WaitForNextTickAsync(stoppingToken))
                    await ProcessAsync(stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                // Normal application shutdown.
            }

            _logger.LogInformation("Session lifecycle background service stopped");
        }

        private async Task ProcessAsync(CancellationToken cancellationToken)
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();

                var processor = scope.ServiceProvider.GetRequiredService<ISessionLifecycleProcessor>();

                await processor.ProcessAsync(cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing session lifecycle");
            }
        }
    }
}
