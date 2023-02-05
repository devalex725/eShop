using YourBrand.Customers.Application.Common.Models;
using YourBrand.Customers.Domain;
using YourBrand.Customers.Domain.Events;

using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Customers.Application.Common.Interfaces;
using YourBrand.Customers.Application.Common;

namespace YourBrand.Customers.Application.Features.Addresses.Events;

public class AddressCreatedHandler : IDomainEventHandler<AddressCreated>
{
    private readonly IApplicationDbContext _context;

    public AddressCreatedHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddressCreated notification, CancellationToken cancellationToken)
    {
        var person = await _context.Addresses
            .FirstOrDefaultAsync(i => i.Id == notification.AddressId);

        if (person is not null)
        {

        }
    }
}