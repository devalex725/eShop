
using YourBrand.Inventory.Domain;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace YourBrand.Inventory.Application.Warehouses.Items.Commands;

public record ReserveWarehouseItems(string Id, int Quantity) : IRequest
{
    public class Handler : IRequestHandler<ReserveWarehouseItems>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReserveWarehouseItems request, CancellationToken cancellationToken)
        {
            var item = await _context.WarehouseItems.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (item is null) throw new Exception();

            item.Reserve(request.Quantity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
