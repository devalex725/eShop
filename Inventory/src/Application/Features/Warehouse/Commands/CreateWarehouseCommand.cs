﻿using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Inventory.Domain;

namespace YourBrand.Inventory.Application.Features.Warehouses.Commands;

public record CreateWarehouseCommand(string Name, string SiteId) : IRequest<WarehouseDto>
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, WarehouseDto>
    {
        private readonly IApplicationDbContext context;

        public CreateWarehouseCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<WarehouseDto> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(i => i.Name == request.Name, cancellationToken);

            if (warehouse is not null) throw new Exception();

            warehouse = new Domain.Entities.Warehouse(Guid.NewGuid().ToString(), request.Name, request.SiteId);

            context.Warehouses.Add(warehouse);

            await context.SaveChangesAsync(cancellationToken);

            warehouse = await context.Warehouses
                .Include(x => x.Site)
                .FirstAsync(i => i.Name == request.Name, cancellationToken);

            return warehouse.ToDto();
        }
    }
}
