using Movie.Domain.Events.Language;

namespace Movie.Domain.Entities.Language
{
    public sealed partial class Language
    {
        public void Update(
            string name,
            string nativeName,
            string isoCode)
        {
            Validate(name, nativeName, isoCode);

            Name = name.Trim();
            NativeName = nativeName.Trim();
            IsoCode = isoCode.Trim().ToUpperInvariant();

            Raise(new LanguageUpdatedEvent(Id));
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            MarkDeleted();

            Raise(new LanguageDeletedEvent(Id));
        }

        public void Restore()
        {
            if (!IsDeleted)
                return;

            MarkRestored();

            Raise(new LanguageRestoredEvent(Id));
        }
    }
}
