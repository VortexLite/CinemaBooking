using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movie.Infrastructure.Messaging.IntegrationEvents;
using Movie.Infrastructure.Messaging.Outbox.Metrics;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxProcessor
        : IOutboxProcessor
    {
        private const int BatchSize = 50;
        private static readonly TimeSpan LockDuration =
            TimeSpan.FromMinutes(1);
        private const int MaxRetryCount = 5;
        private readonly MovieDbContext _dbContext;
        private readonly IOutboxSerializer _serializer;
        private readonly IIntegrationEventPublisher _publisher;
        private readonly ILogger<OutboxProcessor> _logger;
        private readonly OutboxMetrics _metric;

        public OutboxProcessor(MovieDbContext dbContext, 
            IOutboxSerializer serializer, 
            IIntegrationEventPublisher publisher, 
            ILogger<OutboxProcessor> logger,
            OutboxMetrics metrics)
        {
            _dbContext = dbContext;
            _serializer = serializer;
            _publisher = publisher;
            _logger = logger;
            _metric = metrics;
        }

        public async Task ProcessAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Outbox processor started.");

            var messages = await ClaimMessagesAsync(cancellationToken);

            _logger.LogInformation("Claimed {Count} outbox messages.",
                messages.Count);

            if (messages.Count == 0)
                return;

            foreach (var message in messages)
            {
                await ProcessMessageAsync(
                    message,
                    cancellationToken);
            }
        }

        private async Task<List<OutboxMessage>> ClaimMessagesAsync(
            CancellationToken cancellationToken)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database
                .BeginTransactionAsync(cancellationToken);

                var messages = await _dbContext.OutboxMessages
                    .FromSqlInterpolated($"""
                        SELECT *
                        FROM movie."OutboxMessages"
                        WHERE "ProcessedOnUtc" IS NULL
                          AND (
                              "LockedUntilUtc" IS NULL
                              OR "LockedUntilUtc" <= NOW()
                          )
                          AND (
                              "NextRetryUtc" IS NULL
                              OR "NextRetryUtc" <= NOW()
                          )
                        ORDER BY "OccurredOnUtc"
                        LIMIT {BatchSize}
                        FOR UPDATE SKIP LOCKED
                        """)
                    .ToListAsync(cancellationToken);

                foreach (var message in messages)
                {
                    message.Lock(LockDuration);
                }

                if (messages.Count > 0)
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);

                return messages;
            });
        }

        private async Task ProcessMessageAsync(
            OutboxMessage message,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var eventType = IntegrationEventRegistry.GetType(
                    message.Type);

                var integrationEvent = _serializer.Deserialize(
                        Convert.FromBase64String(message.Content),
                        eventType);

                await _publisher.PublishAsync(message.Type,
                    integrationEvent,
                    cancellationToken);

                message.MarkProcessed();

                await _dbContext.SaveChangesAsync(cancellationToken);

                _metric.Published(message.Type);

                _metric.RecordPublishLag(message.Type, DateTime.UtcNow - message.OccurredOnUtc);

                _logger.LogInformation(
                    "Outbox message {MessageId} processed successfully. Event: {EventType}",
                    message.Id,
                    message.Type);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _metric.Failed(message.Type);

                int failedAttempt = message.RetryCount + 1;

                _logger.LogError(
                    ex,
                    "Failed processing Outbox message {MessageId}. Event: {EventType}. Attempt {Attempt}/{MaxAttempts}",
                    message.Id,
                    message.Type,
                    failedAttempt,
                    MaxRetryCount);

                if (failedAttempt >= MaxRetryCount)
                {
                    await MoveToDeadLetterAsync(
                        message,
                        ex,
                        failedAttempt,
                        cancellationToken);

                    _metric.DeadLettered(message.Type);

                    return;
                }

                TimeSpan retryDelay = GetRetryDelay(failedAttempt);

                message.MarkFailed(
                    ex.ToString(),
                    retryDelay);

                await _dbContext.SaveChangesAsync(cancellationToken);

                _metric.Retried(message.Type);

                _logger.LogWarning(
                    "Outbox message {MessageId} scheduled for retry in {RetryDelay}. Attempt {Attempt}/{MaxAttempts}",
                    message.Id,
                    retryDelay,
                    failedAttempt,
                    failedAttempt,
                    MaxRetryCount);
            }
            finally
            {
                stopwatch.Stop();

                _metric.RecordProcessingDuration(message.Type,
                    stopwatch.Elapsed);
            }
        }

        private static TimeSpan GetRetryDelay(int retryCount)
        {
            return retryCount switch
            {
                1 => TimeSpan.FromSeconds(5),
                2 => TimeSpan.FromSeconds(15),
                3 => TimeSpan.FromSeconds(30),
                4 => TimeSpan.FromMinutes(1),
                _ => TimeSpan.FromMinutes(5)
            };
        }

        private async Task MoveToDeadLetterAsync(OutboxMessage message,
            Exception exception,
            int retryCount,
            CancellationToken cancellationToken)
        {
            var deadLetter = new OutboxDeadLetter(
                message.Id,
                message.Type,
                message.Content,
                message.OccurredOnUtc,
                retryCount,
                exception.ToString());

            await _dbContext.OutboxDeadLetters.AddAsync(deadLetter);

            _dbContext.OutboxMessages.Remove(message);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogError(
                "Outbox message {MessageId} moved to dead letter after {RetryCount} failed attempts.",
                message.Id,
                retryCount);
        }
    }
}
