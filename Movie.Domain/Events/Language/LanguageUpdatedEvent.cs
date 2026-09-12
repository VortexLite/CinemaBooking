using Movie.Domain.Common;

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
