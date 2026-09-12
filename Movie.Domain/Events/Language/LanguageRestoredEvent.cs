using Movie.Domain.Common;

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
