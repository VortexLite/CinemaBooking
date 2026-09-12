using Shared.Contracts.IntegrationEvents.Movies;
using Shared.Contracts.IntegrationEvents.Sessions;

namespace Shared.Contracts.IntegrationEvents
{
    public static class IntegrationEventRegistry
    {
        private static readonly IReadOnlyDictionary<Type, string> EventNames =
            new Dictionary<Type, string>
            {
                [typeof(MoviePublishedIntegrationEvent)] = IntegrationEventNames.MoviePublished,

                [typeof(SessionBookingOpenedIntegrationEvent)] = IntegrationEventNames.SessionBookingOpened,

                [typeof(SessionCancelledIntegrationEvent)] = IntegrationEventNames.SessionCancelled,

                [typeof(SessionRescheduledIntegrationEvent)] = IntegrationEventNames.SessionRescheduled,

                [typeof(SessionHallChangedIntegrationEvent)] = IntegrationEventNames.SessionHallChanged
            };

        private static readonly IReadOnlyDictionary<string, Type>
            EventTypes = EventNames.ToDictionary(
                    x => x.Value,
                    x => x.Key);

        public static string GetName(Type eventType)
        {
            if (!EventNames.TryGetValue(eventType,out var eventName))
                throw new InvalidOperationException($"Integration event '{eventType.Name}' is not registered.");

            return eventName;
        }

        public static Type GetType(string eventName)
        {
            if (!EventTypes.TryGetValue(eventName,out var eventType))
                throw new InvalidOperationException($"Integration event '{eventName}' is not registered.");

            return eventType;
        }
    }
}
