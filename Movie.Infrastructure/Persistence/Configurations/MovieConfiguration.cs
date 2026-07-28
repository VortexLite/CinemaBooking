using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Infrastructure.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Domain.Entities.Movie.Movie>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Movie.Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(3000)
                .IsRequired();

            builder.Property(x => x.DurationMinutes)
                .IsRequired();

            builder.Property(x => x.AgeRestriction)
                .IsRequired();

            builder.Property(x => x.PosterUrl)
                .HasMaxLength(500);

            builder.Property(x => x.TrailerUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ReleaseDate);

            builder.Property(x => x.Status)
                .HasConversion<byte>();

            builder.Property(x => x.Language)
                .HasConversion<byte>();

            builder.HasMany(x => x.Sessions)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MovieGenres)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.Language)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Title);
        }
    }
}
