using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken = default);
    }
}
