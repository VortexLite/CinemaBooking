using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Language
{
    public sealed class LanguageRestoredEvent : DomainEvent
    {
        public Guid LanguageId { get; }

        public LanguageRestoredEvent(Guid languageId)
        {
            LanguageId = languageId;
        }
    }
}
