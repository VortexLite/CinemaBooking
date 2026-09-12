using Movie.Domain.Common;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionPriceChangedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public decimal Price { get; }

        public SessionPriceChangedEvent(Guid sessionId, decimal price)
        {
            SessionId = sessionId;
            Price = price;
        }
    }
}
