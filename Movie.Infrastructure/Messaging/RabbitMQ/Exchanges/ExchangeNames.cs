using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.RabbitMQ.Exchanges
{
    public static class ExchangeNames
    {
        public const string MovieEvents = "movie.events";

        public const string MovieDeadLetter = "movie.dead";
    }
}
