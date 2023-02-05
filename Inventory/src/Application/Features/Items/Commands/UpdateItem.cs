﻿
using YourBrand.Inventory.Domain;

using MediatR;
using YourBrand.Inventory.Application.Features.Items;
using Microsoft.EntityFrameworkCore;

namespace YourBrand.Inventory.Application.Features.Items.Commands;

public record UpdateItem(string Id, string NewId, string Name, string GroupId, string Unit) : IRequest
{
    public class Handler : IRequestHandler<UpdateItem>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItem request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (item is null) throw new Exception();

            item.Name = request.Name;
            item.GroupId = request.GroupId;
            item.Unit = request.Unit;

            await _context.SaveChangesAsync(cancellationToken);

            return MediatR.Unit.Value;
        }
    }
}
