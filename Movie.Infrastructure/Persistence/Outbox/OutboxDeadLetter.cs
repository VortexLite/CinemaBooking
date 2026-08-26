using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxDeadLetter
    {
        public Guid Id { get; private set; }

        public Guid OriginalMessageId { get; private set; }

        public string Type { get; private set; } = null!;

        public string Content { get; private set; } = null!;

        public DateTime OccurredOnUtc { get; private set; }

        public DateTime FailedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string Error { get; private set; } = null!;

        private OutboxDeadLetter() { }

        public OutboxDeadLetter(
            Guid originalMessageId,
            string type,
            string content,
            DateTime occurredOnUtc,
            int retryCount,
            string error)
        {
            Id = Guid.NewGuid();

            OriginalMessageId = originalMessageId;

            Type = type;
            Content = content;

            OccurredOnUtc = occurredOnUtc;
            FailedOnUtc = DateTime.UtcNow;

            RetryCount = retryCount;

            Error = error;
        }
    }
}
