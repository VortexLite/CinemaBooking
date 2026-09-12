using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieAgeRestrictionChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public byte AgeRestriction { get; }

        public MovieAgeRestrictionChangedEvent(
            Guid movieId,
            byte ageRestriction)
        {
            MovieId = movieId;
            AgeRestriction = ageRestriction;
        }
    }
}
