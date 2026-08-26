using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Language
{
    public sealed class LanguageDeletedEvent : DomainEvent
    {
        public Guid LanguageId { get; }

        public LanguageDeletedEvent(Guid languageId)
        {
            LanguageId = languageId;
        }
    }
}
