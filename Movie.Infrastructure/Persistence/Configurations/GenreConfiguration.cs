using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Genre;
using Movie.Infrastructure.Persistence.Extensions;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Metadata
                .FindNavigation(nameof(Genre.MovieGenres))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
