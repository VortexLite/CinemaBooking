namespace Movie.Infrastructure.Messaging.RabbitMQ
{
    public static class RoutingKeys
    {
        public const string MovieCreated = "movie.created";

        public const string MovieUpdated = "movie.updated";

        public const string MovieDeleted = "movie.deleted";

        public const string SessionCreated = "session.created";
    }
}
