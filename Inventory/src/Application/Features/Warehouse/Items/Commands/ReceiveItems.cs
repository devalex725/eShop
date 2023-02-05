
using YourBrand.Inventory.Domain;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace YourBrand.Inventory.Application.Features.Warehouses.Items.Commands;

public record ReceiveWarehouseItems(string Id, int Quantity) : IRequest
{
    public class Handler : IRequestHandler<ReceiveWarehouseItems>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReceiveWarehouseItems request, CancellationToken cancellationToken)
        {
            var item = await _context.WarehouseItems.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (item is null) throw new Exception();

            item.Receive(request.Quantity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
