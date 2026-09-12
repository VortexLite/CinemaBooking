using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionUpdatedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionUpdatedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
