﻿using YourBrand.Inventory.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace YourBrand.Inventory.Application.Features.Items.Groups.Commands;

public record DeleteItemGroup(string ItemGroupId) : IRequest
{
    public class Handler : IRequestHandler<DeleteItemGroup>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteItemGroup request, CancellationToken cancellationToken)
        {
            /*
            var invoice = await _context.ItemGroups
                //.Include(i => i.Addresses)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ItemGroupId, cancellationToken);

            if(invoice is null)
            {
                throw new Exception();
            }

            _context.ItemGroups.Remove(invoice);

            await _context.SaveChangesAsync(cancellationToken);
            */

            return Unit.Value;
        }
    }
}