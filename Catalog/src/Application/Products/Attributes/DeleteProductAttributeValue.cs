using MediatR;

using Microsoft.EntityFrameworkCore;

using Catalog.Domain;

namespace Catalog.Application.Products.Attributes;

public record DeleteProductAttributeValue(string ProductId, string AttributeId, string ValueId) : IRequest
{
    public class Handler : IRequestHandler<DeleteProductAttributeValue>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductAttributeValue request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
             .AsSplitQuery()
             .Include(pv => pv.Attributes)
             .ThenInclude(pv => pv.Values)
             .FirstAsync(p => p.Id == request.ProductId);

            var attribute = product.Attributes.First(o => o.Id == request.AttributeId);

            var value = attribute.Values.First(o => o.Id == request.ValueId);

            attribute.Values.Remove(value);
            _context.AttributeValues.Remove(value);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
