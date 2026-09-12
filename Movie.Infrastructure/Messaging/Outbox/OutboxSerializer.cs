using MessagePack;
using MessagePack.Resolvers;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxSerializer : IOutboxSerializer
    {
        private static readonly MessagePackSerializerOptions SerializerOptions =
            MessagePackSerializerOptions.Standard
                .WithResolver(ContractlessStandardResolver.Instance);

        public byte[] Serialize(object value, Type type)
        {
            return MessagePackSerializer.Serialize(type, value, SerializerOptions);
        }

        public object Deserialize(byte[] bytes, Type type)
        {
            return MessagePackSerializer.Deserialize(type, bytes, SerializerOptions)!;
        }
    }
}
