using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionRestoredEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionRestoredEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
