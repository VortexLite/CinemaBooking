using RabbitMQ.Client;

namespace Movie.Infrastructure.Messaging.RabbitMQ
{
    public sealed class RabbitMqConnection
        : IRabbitMqConnection, IAsyncDisposable
    {
        private readonly IConnection _connection;

        public RabbitMqConnection(IConnection connection)
        {
            _connection = connection;
        }

        public async ValueTask<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
        {
            return await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        }

        public ValueTask DisposeAsync()
        {
            return _connection.DisposeAsync();
        }
    }
}
