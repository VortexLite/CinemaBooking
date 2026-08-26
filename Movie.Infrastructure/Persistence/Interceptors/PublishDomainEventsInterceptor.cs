using MediatR;
using Microsoft.EntityFrameworkCore;
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

            var domainEvents = GetDomainEvents(eventData.Context);

            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

            return await base.SavedChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        private static List<IDomainEvent> GetDomainEvents(DbContext context)
        {
            var aggregates = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Count > 0)
                .ToList();

            var events = aggregates
                .SelectMany(x => x.DomainEvents)
                .ToList();

            foreach (var aggregate in aggregates)
            {
                aggregate.ClearEvents();
            }

            return events;
        }
    }
}
