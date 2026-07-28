using Movie.Domain.Common;
using Movie.Domain.Enums;
using Movie.Domain.Events.Session;
using Movie.Domain.Extensions;

namespace Movie.Domain.Entities.Session
{
    public sealed partial class Session : AggregateRoot
    {
        public Guid MovieId { get; private set; }
        public Movie.Movie Movie { get; private set; } = null!;
        public Guid HallId { get; private set; }
        public Hall.Hall Hall { get; private set; } = null!;
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public decimal Price { get; private set; }
        public SessionStatus Status { get; private set; }
        public ProjectionType ProjectionType { get; private set; }
        public SubtitleType SubtitleType { get; private set; }

        public SoundFormat SoundFormat { get; private set; }

        private Session() { }
        public Session(
            Guid movieId,
            Guid hallId,
            DateTime startTime,
            DateTime endTime,
            decimal price)
        {
            if (startTime >= endTime)
                throw new DomainException("Invalid session time.");

            var session = new Session
            {
                MovieId = movieId,
                HallId = hallId,
                StartTime = startTime,
                EndTime = endTime,
                Price = price,
                Status = SessionStatus.Scheduled
            };

            Raise(new SessionCreatedEvent(session.Id));
        }
    }
}
