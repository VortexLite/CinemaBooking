using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionCancelledEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public string Reason { get; }

        public SessionCancelledEvent(Guid sessionId, string reason)
        {
            SessionId = sessionId;
            Reason = reason;
        }
    }
}
