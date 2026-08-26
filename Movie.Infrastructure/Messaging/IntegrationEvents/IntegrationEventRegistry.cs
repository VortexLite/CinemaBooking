using Movie.Domain.Events.Genre;
using Movie.Domain.Events.Hall;
using Movie.Domain.Events.Language;
using Movie.Domain.Events.Movies;
using Movie.Domain.Events.Session;
using Shared.Contracts.IntegrationEvents.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.IntegrationEvents
{
    public static class IntegrationEventRegistry
    {
        private static readonly IReadOnlyDictionary<Type, string> TypeToName = 
            new Dictionary<Type, string>
        {
            [typeof(MovieCreatedEvent)] = IntegrationEventNames.MovieCreated,
            [typeof(MovieUpdatedEvent)] = IntegrationEventNames.MovieUpdated,
            [typeof(MovieDeletedEvent)] = IntegrationEventNames.MovieDeleted,
            [typeof(MoviePublishedIntegrationEvent)] = IntegrationEventNames.MoviePublished,
            [typeof(MovieRestoredEvent)] = IntegrationEventNames.MovieRestored,
            [typeof(MovieArchivedEvent)] = IntegrationEventNames.MovieArchived,

            [typeof(MovieGenreAddedEvent)] = IntegrationEventNames.MovieGenreAdded,
            [typeof(MovieGenreRemovedEvent)] = IntegrationEventNames.MovieGenreRemoved,

            [typeof(MovieAgeRestrictionChangedEvent)] = IntegrationEventNames.MovieAgeRestrictionChanged,
            [typeof(MovieDescriptionChangedEvent)] = IntegrationEventNames.MovieDescriptionChanged,
            [typeof(MovieDurationChangedEvent)] = IntegrationEventNames.MovieDurationChanged,
            [typeof(MovieLanguageChangedEvent)] = IntegrationEventNames.MovieLanguageChanged,
            [typeof(MoviePosterChangedEvent)] = IntegrationEventNames.MoviePosterChanged,
            [typeof(MovieReleaseDateChangedEvent)] = IntegrationEventNames.MovieReleaseDateChanged,
            [typeof(MovieTitleChangedEvent)] = IntegrationEventNames.MovieTitleChanged,
            [typeof(MovieTrailerChangedEvent)] = IntegrationEventNames.MovieTrailerChanged,

            [typeof(GenreCreatedEvent)] = IntegrationEventNames.GenreCreated,
            [typeof(GenreDeletedEvent)] = IntegrationEventNames.GenreDeleted,
            [typeof(GenreRenamedEvent)] = IntegrationEventNames.GenreRenamed,

            [typeof(HallClosedEvent)] = IntegrationEventNames.HallClosed,
            [typeof(HallCreatedEvent)] = IntegrationEventNames.HallCreated,
            [typeof(HallLayoutChangedEvent)] = IntegrationEventNames.HallLayoutChanged,
            [typeof(HallRenamedEvent)] = IntegrationEventNames.HallRenamed,

            [typeof(SeatAddedEvent)] = IntegrationEventNames.SeatAdded,
            [typeof(SeatRemovedEvent)] = IntegrationEventNames.SeatRemoved,
            [typeof(SeatTypeChangedEvent)] = IntegrationEventNames.SeatTypeChanged,

            [typeof(LanguageCreatedEvent)] = IntegrationEventNames.LanguageCreated,
            [typeof(LanguageDeletedEvent)] = IntegrationEventNames.LanguageDeleted,
            [typeof(LanguageUpdatedEvent)] = IntegrationEventNames.LanguageUpdated,

            [typeof(SessionBookingOpenedEvent)] = IntegrationEventNames.SessionBookingOpened,
            [typeof(SessionCancelledEvent)] = IntegrationEventNames.SessionCancelled,
            [typeof(SessionCreatedEvent)] = IntegrationEventNames.SessionCreated,
            [typeof(SessionFinishedEvent)] = IntegrationEventNames.SessionFinished,
            [typeof(SessionHallChangedEvent)] = IntegrationEventNames.SessionHallChanged,
            [typeof(SessionPriceChangedEvent)] = IntegrationEventNames.SessionPriceChanged,
            [typeof(SessionRescheduledEvent)] = IntegrationEventNames.SessionRescheduled,
            [typeof(SessionStartedEvent)] = IntegrationEventNames.SessionStarted
        };

        private static readonly IReadOnlyDictionary<string, Type> NameToType =
            TypeToName.ToDictionary(
                x => x.Value,
                x => x.Key);

        public static string GetName(Type type)
        {
            if (!TypeToName.TryGetValue(type, out string? eventName))
                throw new InvalidOperationException(
                    $"Unknown integration event type '{type.Name}'.");

            return eventName;
        }

        public static Type GetType(string eventName)
        {
            if (!NameToType.TryGetValue(eventName, out Type? eventType))
                throw new InvalidOperationException(
                    $"Unknown integration event '{eventName}'.");

            return eventType;
        }
    }
}
