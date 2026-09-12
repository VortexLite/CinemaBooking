using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movie.Infrastructure.Messaging.Outbox.Metrics;
using Movie.Infrastructure.Persistence;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxCleanupService
        : IOutboxCleanupService
    {
        private static readonly TimeSpan RetentionPeriod = TimeSpan.FromDays(7);
        private readonly MovieDbContext _dbContext;
        private readonly ILogger<OutboxCleanupService> _logger;
        private readonly OutboxMetrics _metrics;

        public OutboxCleanupService(MovieDbContext dbContext, 
            ILogger<OutboxCleanupService> logger,
            OutboxMetrics metrics)
        {
            _dbContext = dbContext;
            _logger = logger;
            _metrics = metrics;
        }

        public async Task CleanupAsync(CancellationToken cancellationToken = default)
        {
            DateTime threshold = DateTime.UtcNow.Subtract(RetentionPeriod);

            int deleted = await _dbContext.OutboxMessages
                .Where(x =>
                    x.ProcessedOnUtc != null &&
                    x.ProcessedOnUtc < threshold)
                .ExecuteDeleteAsync(cancellationToken);

            if (deleted > 0)
            {
                _metrics.Cleaned(deleted);

                _logger.LogInformation("Deleted {Count} processed Outbox messages older than {Threshold}.",
                    deleted, threshold);
            }
        }
    }
}
