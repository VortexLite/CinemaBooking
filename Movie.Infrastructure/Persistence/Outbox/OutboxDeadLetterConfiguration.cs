using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxDeadLetterConfiguration
        : IEntityTypeConfiguration<OutboxDeadLetter>
    {
        public void Configure(EntityTypeBuilder<OutboxDeadLetter> builder)
        {
            builder.ToTable("OutboxDeadLetters");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalMessageId)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.OccurredOnUtc)
                .IsRequired();

            builder.Property(x => x.FailedOnUtc)
                .IsRequired();

            builder.Property(x => x.RetryCount)
                .IsRequired();

            builder.Property(x => x.Error)
                .HasColumnType("text")
                .IsRequired();

            builder.HasIndex(x => x.OriginalMessageId)
                .IsUnique();

            builder.HasIndex(x => x.FailedOnUtc);

            builder.HasIndex(x => x.Type);
        }
    }
}
