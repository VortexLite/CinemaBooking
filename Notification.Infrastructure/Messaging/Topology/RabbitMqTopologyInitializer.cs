using Notification.Infrastructure.Messaging.Queues;
using Notification.Infrastructure.Messaging.RabbitMQ.Bindings;
using Notification.Infrastructure.Messaging.RabbitMQ.Exchanges;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Infrastructure.Messaging.Topology
{
    public sealed class RabbitMqTopologyInitializer
        : IRabbitMqTopologyInitializer
    {
        private readonly IConnection _connection;

        public RabbitMqTopologyInitializer(IConnection connection)
        {
            _connection = connection;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            await using var channel = await _connection.CreateChannelAsync(
                cancellationToken: cancellationToken);

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeNames.MovieEvents,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(
                queue: QueueNames.MovieEvents,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            await channel.QueueBindAsync(
                queue: QueueNames.MovieEvents,
                exchange: ExchangeNames.MovieEvents,
                routingKey: BindingKeys.MovieEvents,
                arguments: null,
                cancellationToken: cancellationToken);
        }
    }
}
