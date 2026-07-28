using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Interceptors
{
    public sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher _publisher;

        public PublishDomainEventsInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return await base.SavedChangesAsync(
                    eventData,
                    result,
                    cancellationToken);

            var aggregates = eventData.Context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToList();

            var domainEvents = aggregates
                .SelectMany(x => x.DomainEvents)
                .ToList();

            foreach (var aggregate in aggregates)
                aggregate.ClearEvents();

            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

            return await base.SavedChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}
