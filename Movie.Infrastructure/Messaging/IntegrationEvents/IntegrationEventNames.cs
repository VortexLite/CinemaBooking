using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.IntegrationEvents
{
    public static class IntegrationEventNames
    {
        public const string MovieCreated = "movie.created";

        public const string MovieUpdated = "movie.updated";

        public const string MovieDeleted = "movie.deleted";

        public const string MoviePublished = "movie.published";

        public const string MovieRestored = "movie.restored";

        public const string MovieArchived = "movie.archive";



        public const string MovieGenreAdded = "movie.genre.added";

        public const string MovieGenreRemoved = "movie.genre.removed";




        public const string MovieAgeRestrictionChanged = "movie.change.ageRestriction";

        public const string MovieDescriptionChanged = "movie.change.description";

        public const string MovieDurationChanged = "movie.change.duration";

        public const string MovieLanguageChanged = "movie.change.language";

        public const string MoviePosterChanged = "movie.change.poster";

        public const string MovieReleaseDateChanged = "movie.change.releaseDate";

        public const string MovieTitleChanged = "movie.change.title";

        public const string MovieTrailerChanged = "movie.change.trailer";



        public const string GenreCreated = "genre.created";

        public const string GenreDeleted = "genre.deleted";

        public const string GenreRenamed = "genre.renamed";



        public const string HallClosed = "hall.closed";

        public const string HallCreated = "hall.created";

        public const string HallLayoutChanged = "hall.change.layout";

        public const string HallRenamed = "hall.renamed";



        public const string SeatAdded = "Seat.added";

        public const string SeatRemoved = "Seat.removed";

        public const string SeatTypeChanged = "Seat.change.type";



        public const string LanguageCreated = "language.created";

        public const string LanguageDeleted = "language.deleted";

        public const string LanguageUpdated = "language.updated";



        public const string SessionBookingOpened = "session.opened";

        public const string SessionCancelled = "session.cancelled";

        public const string SessionCreated = "session.created";

        public const string SessionFinished = "session.finished";

        public const string SessionHallChanged = "session.change.hall";

        public const string SessionPriceChanged = "session.change.price";

        public const string SessionRescheduled = "session.reschedule";

        public const string SessionStarted = "session.started";
    }
}
