using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxCleanupService
    {
        Task CleanupAsync(CancellationToken cancellationToken = default);
    }
}
