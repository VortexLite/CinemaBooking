using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Session;
using Movie.Infrastructure.Persistence.Extensions;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(10, 2);

            builder.Property(x => x.Status)
                .HasConversion<byte>();

            builder.Property(x => x.ProjectionType)
                .HasConversion<byte>()
                .IsRequired();

            builder.Property(x => x.SoundFormat)
                .HasConversion<byte>()
                .IsRequired();

            builder.Property(x => x.SubtitleType)
                .HasConversion<byte>()
                .IsRequired();

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Hall)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.HallId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.MovieId,
                x.HallId,
                x.StartTime
            });
        }
    }
}
