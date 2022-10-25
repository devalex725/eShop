﻿using YourBrand.Inventory.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Inventory.Application.Items;

namespace YourBrand.Inventory.Application.Items.Queries;

public record GetItem(string ItemId) : IRequest<ItemDto?>
{
    public class Handler : IRequestHandler<GetItem, ItemDto?>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemDto?> Handle(GetItem request, CancellationToken cancellationToken)
        {
            var person = await _context.Items
                .Include(x => x.Group)
                .Include(x => x.WarehouseItems)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

            return person is null
                ? null
                : person.ToDto();
        }
    }
}