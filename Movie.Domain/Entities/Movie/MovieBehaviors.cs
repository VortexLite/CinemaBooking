using Movie.Domain.Enums;
using Movie.Domain.Events.Movie;
using Movie.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {
        public void Publish()
        {
            if (Status == MovieStatus.Published)
                throw new DomainException("Movie already published.");

            Status = MovieStatus.Published;

            Raise(new MoviePublishedEvent(Id));
        }

        public void Archive()
        {
            if (Status == MovieStatus.Archived)
                return;

            Status = MovieStatus.Archived;

            Raise(new MovieArchivedEvent(Id));
        }

        public void ChangeTitle(string title)
        {
            ValidateTitle(title);

            Title = title.Trim();
        }

        public void ChangeDescription(string description)
        {
            Description = description.Trim();
        }

        public void ChangePoster(string poster)
        {
            ValidatePoster(poster);

            PosterUrl = poster;
        }

        public void ChangeTrailer(string? trailer)
        {
            TrailerUrl = trailer;
        }
    }
}
