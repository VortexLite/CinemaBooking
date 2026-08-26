using Microsoft.EntityFrameworkCore.Diagnostics;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Common;
using Movie.Infrastructure.Messaging.Outbox;
using Movie.Infrastructure.Persistence.Outbox;
using Shared.Contracts.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor
        : SaveChangesInterceptor
    {
        private readonly IDomainEventToIntegrationEventMapper _mapper;
        private readonly IOutboxMessageSerializer _outboxMessageSerializer;

        public ConvertDomainEventsToOutboxMessagesInterceptor(
            IDomainEventToIntegrationEventMapper mapper, 
            IOutboxMessageSerializer outboxMessageSerializer)
        {
            _mapper = mapper;
            _outboxMessageSerializer = outboxMessageSerializer;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData,
                    result,
                    cancellationToken);
            }

            List<AggregateRoot> aggregates =
                eventData.Context.ChangeTracker
                    .Entries<AggregateRoot>()
                    .Select(x => x.Entity)
                    .Where(x => x.DomainEvents.Count > 0)
                    .ToList();

            if (aggregates.Count == 0)
            {
                return base.SavingChangesAsync(
                    eventData,
                    result,
                    cancellationToken);
            }

            List<OutboxMessage> outboxMessages = [];

            foreach (AggregateRoot aggregate in aggregates)
            {
                foreach (IDomainEvent domainEvent in aggregate.DomainEvents)
                {
                    IIntegrationEvent? integrationEvent =
                        _mapper.Map(domainEvent);

                    if (integrationEvent is null)
                        continue;

                    OutboxMessage outboxMessage =
                        _outboxMessageSerializer.Serialize(
                            integrationEvent);

                    outboxMessages.Add(outboxMessage);
                }
            }

            if (outboxMessages.Count > 0)
            {
                eventData.Context
                    .Set<OutboxMessage>()
                    .AddRange(outboxMessages);
            }

            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}
