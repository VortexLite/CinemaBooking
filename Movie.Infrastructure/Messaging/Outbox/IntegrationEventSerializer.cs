using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class IntegrationEventSerializer
        : IIntegrationEventSerializer
    {
        private static readonly MessagePackSerializerOptions Options =
            MessagePackSerializerOptions.Standard
                .WithResolver(ContractlessStandardResolver.Instance);

        public byte[] Serialize(object integrationEvent)
        {
            return MessagePackSerializer.Serialize(
                integrationEvent,
                Options);
        }

        public object Deserialize(byte[] playload, Type type)
        {
            return MessagePackSerializer.Deserialize(
                type,
                playload,
                Options);
        }
    }
}
