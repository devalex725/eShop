﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace YourBrand.Customers.Infrastructure.Services;

sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger;
    private readonly IPublisher _mediator;

    public DomainEventDispatcher(ILogger<DomainEventDispatcher> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Dispatch(DomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}