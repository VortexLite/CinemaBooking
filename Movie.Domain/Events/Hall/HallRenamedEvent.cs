using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallRenamedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public string Name { get; }

        public HallRenamedEvent(Guid hallId, string name)
        {
            HallId = hallId;
            Name = name;
        }
    }
}
