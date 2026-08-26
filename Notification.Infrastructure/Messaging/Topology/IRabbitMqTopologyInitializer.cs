using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrastructure.Messaging.Topology
{
    public interface IRabbitMqTopologyInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }
}
