namespace Shared.Contracts.IntegrationEvents.Sessions
{
    public sealed class SessionRescheduledIntegrationEvent
        : IntegrationEvent
    {
        public Guid SessionId { get; init; }
        public Guid HallId { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime Endtime { get; init; }

        public SessionRescheduledIntegrationEvent(
            Guid sessionId,
            Guid hallId,
            DateTime startTime,
            DateTime endTime)
        {
            SessionId = sessionId;
            HallId = hallId;
            StartTime = startTime;
            Endtime = endTime;
        }
    }
}
