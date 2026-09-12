using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents
{
    public static class IntegrationEventNames
    {
        public const string MoviePublished = "movie.published";

        public const string SessionBookingOpened = "session.booking-opened";

        public const string SessionCancelled = "session.cancelled";

        public const string SessionRescheduled = "session.rescheduled";

        public const string SessionHallChanged = "session.hall-changed";
    }
}
