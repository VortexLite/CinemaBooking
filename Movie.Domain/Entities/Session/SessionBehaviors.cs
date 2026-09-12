using Movie.Domain.Enums;
using Movie.Domain.Events.Session;
using Movie.Domain.Exceptions;

namespace Movie.Domain.Entities.Session
{
    public sealed partial class Session
    {
        public void Update(
            Guid hallId,
            DateTime startTime,
            DateTime endTime,
            decimal price,
            ProjectionType projectionType,
            SubtitleType subtitleType,
            SoundFormat soundFormat)
        {
            if (Status is SessionStatus.Started
                or SessionStatus.Finished
                or SessionStatus.Cancelled)
                throw new DomainException("This session cannot be modified.");

            Validate(
                MovieId,
                hallId,
                startTime,
                endTime,
                price,
                projectionType,
                subtitleType,
                soundFormat);

            ChangeHall(hallId);
            Reschedule(startTime, endTime);
            ChangePrice(price);
            ChangeProjectionType(projectionType);
            ChangeSubtitleType(subtitleType);
            ChangeSoundFormat(soundFormat);
        }

        public void ChangeHall(Guid hallId)
        {
            EnsureCanBeModified();

            if (hallId == Guid.Empty)
                throw new DomainException("Hall id is required.");

            if (HallId == hallId)
                return;

            if (Status != SessionStatus.Scheduled)
                throw new DomainException("Hall can only be changed for a scheduled session.");

            HallId = hallId;

            Raise(new SessionHallChangedEvent(Id, hallId));
        }

        public void Reschedule(
            DateTime startTime,
            DateTime endTime)
        {
            EnsureCanBeModified();

            if (startTime.Kind != DateTimeKind.Utc)
                throw new DomainException("Session start time must be UTC.");

            if (endTime.Kind != DateTimeKind.Utc)
                throw new DomainException("Session end time must be UTC.");

            if (endTime <= startTime)
                throw new DomainException("Session end time must be after start time.");

            if (StartTime == startTime &&
                EndTime == endTime)
                return;

            StartTime = startTime;
            EndTime = endTime;

            Raise(new SessionRescheduledEvent(Id, HallId, startTime, endTime));
        }

        public void ChangePrice(decimal price)
        {
            EnsureCanBeModified();

            if (price <= 0)
                throw new DomainException("Session price must be greater than zero.");

            if (Price == price)
                return;

            Price = price;

            Raise(new SessionPriceChangedEvent(Id, price));
        }

        public void ChangeProjectionType(
            ProjectionType projectionType)
        {
            EnsureCanBeModified();

            if (!Enum.IsDefined(projectionType))
                throw new DomainException("Invalid projection type.");

            if (ProjectionType == projectionType)
                return;

            ProjectionType = projectionType;
        }

        public void ChangeSubtitleType(
            SubtitleType subtitleType)
        {
            EnsureCanBeModified();

            if (!Enum.IsDefined(subtitleType))
                throw new DomainException("Invalid subtitle type.");

            if (SubtitleType == subtitleType)
                return;

            SubtitleType = subtitleType;
        }

        public void ChangeSoundFormat(
            SoundFormat soundFormat)
        {
            EnsureCanBeModified();

            if (!Enum.IsDefined(soundFormat))
                throw new DomainException( "Invalid sound format.");

            if (SoundFormat == soundFormat)
                return;

            SoundFormat = soundFormat;
        }

        public void OpenBooking()
        {
            if (Status != SessionStatus.Scheduled)
                throw new DomainException("Only scheduled sessions can be opened for booking.");

            Status = SessionStatus.OpenForBooking;

            Raise(new SessionBookingOpenedEvent(Id, MovieId, HallId, StartTime, EndTime, Price));
        }

        public void Cancel(string reason)
        {
            if (Status is SessionStatus.Started
                or SessionStatus.Finished
                or SessionStatus.Cancelled)
                throw new DomainException("This session cannot be cancelled.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("Cancellation reason is required.");

            Status = SessionStatus.Cancelled;

            Raise(new SessionCancelledEvent(Id, reason.Trim()));
        }

        public void Start()
        {
            if (Status is not (
                SessionStatus.Scheduled or
                SessionStatus.OpenForBooking))
                throw new DomainException("Only scheduled or open for booking sessions can be started.");

            Status = SessionStatus.Started;

            Raise(new SessionStartedEvent(Id));
        }

        public void Finish()
        {
            if (Status != SessionStatus.Started)
                throw new DomainException("Only a started session can be finished.");

            Status = SessionStatus.Finished;

            Raise(new SessionFinishedEvent(Id));
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            MarkDeleted();

            Raise(new SessionDeletedEvent(Id));
        }

        public void Restore()
        {
            if (!IsDeleted)
                return;

            MarkRestored();

            Raise(new SessionRestoredEvent(Id));
        }

        private void EnsureCanBeModified()
        {
            if (Status is SessionStatus.Started
                or SessionStatus.Finished
                or SessionStatus.Cancelled)
                throw new DomainException("This session cannot be modified.");
        }
    }
}
