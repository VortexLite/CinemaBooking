using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        ValueTask<IChannel> CreateChannelAsync(
            CancellationToken cancellationToken = default);
    }
}
