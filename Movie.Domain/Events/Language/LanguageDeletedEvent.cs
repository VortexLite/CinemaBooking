using Movie.Domain.Common;

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
