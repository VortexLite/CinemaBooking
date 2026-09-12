using MessagePack;
using MessagePack.Resolvers;
using Movie.Application.Common.Interfaces;

namespace Movie.Infrastructure.Caching
{
    public sealed class MessagePackCacheSerializer : ICacheSerializer
    {
        private static readonly MessagePackSerializerOptions SerializerOptions =
            MessagePackSerializerOptions.Standard
                .WithResolver(ContractlessStandardResolver.Instance)
                .WithCompression(MessagePackCompression.Lz4BlockArray);

        public byte[] Serialize<T>(T value)
        {
            return MessagePackSerializer.Serialize(value, SerializerOptions);
        }

        public T? Deserialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes, SerializerOptions);
        }
    }
}
