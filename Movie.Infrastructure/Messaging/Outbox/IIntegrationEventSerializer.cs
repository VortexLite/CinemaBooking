namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IIntegrationEventSerializer
    {
        byte[] Serialize(object integrationEvent);

        object Deserialize(byte[] playload, Type type);
    }
}
