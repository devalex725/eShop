using MediatR;

using Microsoft.EntityFrameworkCore;

namespace YourBrand.Catalog.Features.Products.Attributes;

public record GetProductAttributes(long ProductId) : IRequest<IEnumerable<ProductAttributeDto>>
{
    public class Handler : IRequestHandler<GetProductAttributes, IEnumerable<ProductAttributeDto>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductAttributeDto>> Handle(GetProductAttributes request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsSplitQuery()
                .AsNoTracking()
                .Include(pv => pv.ProductAttributes)
                .ThenInclude(pv => pv.Value)
                .Include(pv => pv.ProductAttributes)
                .ThenInclude(pv => pv.Attribute)
                .ThenInclude(pv => pv.Group)
                .Include(pv => pv.ProductAttributes)
                .ThenInclude(pv => pv.Attribute)
                .ThenInclude(pv => pv.Values)
                .FirstAsync(p => p.Id == request.ProductId, cancellationToken);

            return product.ProductAttributes.Select(x => x.ToDto());
        }
    }
}