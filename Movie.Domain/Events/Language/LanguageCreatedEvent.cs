using Movie.Domain.Common;

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
