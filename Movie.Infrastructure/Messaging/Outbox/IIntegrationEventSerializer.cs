using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IIntegrationEventSerializer
    {
        byte[] Serialize(object integrationEvent);

        object Deserialize(byte[] playload, Type type);
    }
}
