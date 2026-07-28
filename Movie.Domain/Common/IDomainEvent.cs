using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
