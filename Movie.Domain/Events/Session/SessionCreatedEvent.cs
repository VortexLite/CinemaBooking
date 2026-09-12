using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionCreatedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionCreatedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
