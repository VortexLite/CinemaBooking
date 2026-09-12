using RabbitMQ.Client;

namespace Movie.Infrastructure.Messaging.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        ValueTask<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default);
    }
}
