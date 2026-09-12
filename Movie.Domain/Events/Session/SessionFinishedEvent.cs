using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionFinishedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionFinishedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
