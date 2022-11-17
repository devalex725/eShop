using MediatR;
using YourBrand.CustomerService.Application.Common;
using YourBrand.CustomerService.Application.Services;

namespace YourBrand.CustomerService.Application.Tickets.EventHandlers;

public sealed class TicketUpdatedEventHandler : IDomainEventHandler<TicketUpdated>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketNotificationService ticketNotificationService;

    public TicketUpdatedEventHandler(ITicketRepository ticketRepository, ITicketNotificationService ticketNotificationService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketNotificationService = ticketNotificationService;
    }

    public async Task Handle(TicketUpdated notification, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.FindByIdAsync(notification.TicketId, cancellationToken);

        if (ticket is null)
            return;

        await ticketNotificationService.Updated(ticket.Id, ticket.Title);
    }
}
