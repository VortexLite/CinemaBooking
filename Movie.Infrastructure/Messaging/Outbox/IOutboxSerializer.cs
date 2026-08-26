using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxSerializer
    {
        byte[] Serialize(object value, Type type);

        object Deserialize(byte[] bytes,Type type);
    }
}
