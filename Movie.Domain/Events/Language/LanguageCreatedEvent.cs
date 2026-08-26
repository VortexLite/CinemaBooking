using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Language
{
    public sealed class LanguageCreatedEvent : DomainEvent
    {
        public Guid LanguageId { get; }

        public LanguageCreatedEvent(Guid languageId)
        {
            LanguageId = languageId;
        }
    }
}
