using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionDeletedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionDeletedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
