using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionHallChangedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public Guid HallId { get; }

        public SessionHallChangedEvent(
            Guid sessionId,
            Guid hallId)
        {
            SessionId = sessionId;
            HallId = hallId;
        }
    }
}
