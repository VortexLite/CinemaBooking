using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionPriceChangedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        public SessionPriceChangedEvent(Guid sessionId, 
            decimal oldPrice, 
            decimal newPrice)
        {
            SessionId = sessionId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
