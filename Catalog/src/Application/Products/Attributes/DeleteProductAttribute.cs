using MediatR;

using Microsoft.EntityFrameworkCore;

using YourBrand.Catalog.Domain;

namespace YourBrand.Catalog.Application.Products.Attributes;

public record DeleteProductAttribute(string ProductId, string AttributeId) : IRequest
{
    public class Handler : IRequestHandler<DeleteProductAttribute>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductAttribute request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(x => x.Attributes)
                .FirstAsync(x => x.Id == request.ProductId);

            var attribute = product.Attributes
                .First(x => x.Id == request.AttributeId);

            product.Attributes.Remove(attribute);
            _context.Attributes.Remove(attribute);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
