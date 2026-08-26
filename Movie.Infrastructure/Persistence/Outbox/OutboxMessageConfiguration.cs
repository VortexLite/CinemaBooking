using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxMessageConfiguration
        : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.OccurredOnUtc)
                .IsRequired();

            builder.Property(x => x.RetryCount)
                .IsRequired();

            builder.Property(x => x.Error);

            builder.HasIndex(x => new
            {
                x.ProcessedOnUtc,
                x.NextRetryUtc,
                x.LockedUntilUtc
            });
        }
    }
}
