using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Language
{
    public sealed class LanguageUpdatedEvent : DomainEvent
    {
        public Guid LanguageId { get; }

        public LanguageUpdatedEvent(Guid languageId)
        {
            LanguageId = languageId;
        }
    }
}
