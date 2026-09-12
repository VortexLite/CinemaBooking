using Movie.Infrastructure.Messaging.RabbitMQ.Exchanges;
using RabbitMQ.Client;

namespace Movie.Infrastructure.Messaging.RabbitMQ.Topology
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
        }
    }
}
