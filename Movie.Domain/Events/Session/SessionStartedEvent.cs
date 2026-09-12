using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionStartedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionStartedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
