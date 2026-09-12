using Movie.Domain.Enums;
using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Session
{
    public sealed partial class Session
    {
        private static void Validate(
            Guid movieId,
            Guid hallId,
            DateTime startTime,
            DateTime endsTime,
            decimal price,
            ProjectionType projectionType,
            SubtitleType subtitleType,
            SoundFormat soundFormat)
        {
            if (movieId == Guid.Empty)
                throw new DomainException("Movie id is required.");

            if (hallId == Guid.Empty)
                throw new DomainException("Hall id is required.");

            if (startTime.Kind != DateTimeKind.Utc)
                throw new DomainException("Session start time must be UTC.");

            if (endsTime.Kind != DateTimeKind.Utc)
                throw new DomainException("Session end time must be UTC.");

            if (endsTime <= startTime)
                throw new DomainException("Session end time must be after start time.");

            if (price <= 0)
                throw new DomainException("Session price must be greater than zero.");

            if (!Enum.IsDefined(projectionType))
                throw new DomainException("Invalid projection type.");

            if (!Enum.IsDefined(subtitleType))
                throw new DomainException("Invalid subtitle type.");

            if (!Enum.IsDefined(soundFormat))
                throw new DomainException("Invalid sound format.");
        }
    }
}
