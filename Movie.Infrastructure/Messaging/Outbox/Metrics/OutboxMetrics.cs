using System.Diagnostics.Metrics;

namespace Movie.Infrastructure.Messaging.Outbox.Metrics
{
    public sealed class OutboxMetrics
    {
        public const string MeterName = "Movie.Outbox";

        private readonly Counter<long> _published;
        private readonly Counter<long> _failed;
        private readonly Counter<long> _retried;
        private readonly Counter<long> _deadLettered;
        private readonly Counter<long> _cleaned;

        private readonly Histogram<double> _processingDuration;
        private readonly Histogram<double> _publishLag;

        public OutboxMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);

            _published = meter.CreateCounter<long>(
                "movie.outbox.published",
                unit: "{message}",
                description: "Number of successfully published Outbox messages.");

            _failed = meter.CreateCounter<long>(
                "movie.outbox.failed",
                unit: "{message}",
                description: "Number of failed Outbox publish attempts.");

            _retried = meter.CreateCounter<long>(
                "movie.outbox.retried",
                unit: "{message}",
                description: "Number of Outbox messages scheduled for retry.");

            _deadLettered = meter.CreateCounter<long>(
                "movie.outbox.dead_lettered",
                unit: "{message}",
                description: "Number of Outbox messages moved to dead letter.");

            _cleaned = meter.CreateCounter<long>(
                "movie.outbox.cleaned",
                unit: "{message}",
                description: "Number of processed Outbox messages removed by cleanup.");


            _processingDuration = meter.CreateHistogram<double>(
                "movie.outbox.processing.duration",
                unit: "ms",
                description: "Outbox message processing duration.");

            _publishLag = meter.CreateHistogram<double>(
                "movie.outbox.publish.lag",
                unit: "s",
                description: "Time between Outbox message creation and successful publishing.");
        }

        public void Published(string eventType)
        {
            _published.Add(1, new KeyValuePair<string, object?>("event.type", eventType));
        }

        public void Failed(string eventType)
        {
            _failed.Add(1, new KeyValuePair<string, object?>("event.type", eventType));
        }

        public void Retried(string eventType)
        {
            _retried.Add(1, new KeyValuePair<string, object?>("event.type", eventType));
        }

        public void DeadLettered(string eventType)
        {
            _deadLettered.Add(1,new KeyValuePair<string, object?>("event.type", eventType));
        }

        public void Cleaned(int count)
        {
            _cleaned.Add(count);
        }



        public void RecordProcessingDuration(string eventType, TimeSpan duration)
        {
            _processingDuration.Record(duration.TotalMilliseconds,
                new KeyValuePair<string, object?>("event.type", eventType));
        }

        public void RecordPublishLag(string eventType, TimeSpan lag)
        {
            _publishLag.Record(lag.TotalSeconds,new KeyValuePair<string, object?>(
                "event.type", eventType));
        }
    }
}
