namespace Movie.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; } = default!;
        public string Content { get; private set; } = default!;
        public DateTime OccurredOnUtc { get; private set; }
        public DateTime? LockedUntilUtc { get; private set; }
        public DateTime? ProcessedOnUtc { get; private set; }
        public int RetryCount { get; private set; }
        public DateTime? NextRetryUtc { get; private set; }
        public string? Error { get; private set; }
        public bool IsProcessed => ProcessedOnUtc.HasValue;

        private OutboxMessage() { }

        public OutboxMessage(
            Guid id,
            string type,
            string content,
            DateTime occurredOnUtc)
        {
            Id = id;
            Type = type;
            Content = content;
            OccurredOnUtc = occurredOnUtc;
        }

        public void Lock(TimeSpan duration)
        {
            LockedUntilUtc = DateTime.UtcNow.Add(duration);
        }

        public void Unlock()
        {
            LockedUntilUtc = null;
        }

        public void MarkProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;

            LockedUntilUtc = null;
            
            NextRetryUtc = null;

            Error = null;
        }

        public void MarkFailed(
            string error,
            TimeSpan retryAfter)
        {
            RetryCount++;

            Error = error;

            LockedUntilUtc = null;

            NextRetryUtc = DateTime.UtcNow.Add(retryAfter);
        }
    }
}
