using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents.Movies
{
    public sealed class MoviePublishedIntegrationEvent
        : IntegrationEvent
    {
        public Guid MovieId { get; }

        public string Title { get; }

        public MoviePublishedIntegrationEvent(Guid movieId, string title)
        {
            MovieId = movieId;
            Title = title;
        }
    }
}
