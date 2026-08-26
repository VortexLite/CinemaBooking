using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.RabbitMQ.Topology
{
    public interface IRabbitMqTopologyInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }
}
