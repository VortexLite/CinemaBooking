using Movie.Application.Common.Interfaces;
using Movie.Infrastructure.Messaging.Outbox;
using Movie.Infrastructure.Messaging.RabbitMQ.Exchanges;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.RabbitMQ
{
    public sealed class RabbitMqPublisher
        : IIntegrationEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IIntegrationEventSerializer _serializer;

        public RabbitMqPublisher(IConnection connection,
            IIntegrationEventSerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;
        }

        public async Task PublishAsync(string eventName, 
            object integrationEvent,
            CancellationToken cancellationToken = default)
        {
            var channelOptions = new CreateChannelOptions(
                publisherConfirmationsEnabled: true,
                publisherConfirmationTrackingEnabled: true);

            await using var channel = await _connection
                .CreateChannelAsync(
                    channelOptions,
                    cancellationToken);

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeNames.MovieEvents,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);

            byte[] body = _serializer.Serialize(integrationEvent);

            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/x-msgpack",
                Type = eventName,
                MessageId = Guid.NewGuid().ToString(),
                CorrelationId = Guid.NewGuid().ToString()
            };

            await channel.BasicPublishAsync(
                exchange: ExchangeNames.MovieEvents,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken);
        }
    }
}
