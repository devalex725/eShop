using MediatR;

using Microsoft.EntityFrameworkCore;

using Catalog.Domain;
using Catalog.Domain.Entities;

namespace Catalog.Application.Products.Groups;

public record CreateProductGroup(string ProductId, ApiCreateProductGroup Data) : IRequest<ProductGroupDto>
{
    public class Handler : IRequestHandler<CreateProductGroup, ProductGroupDto>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductGroupDto> Handle(CreateProductGroup request, CancellationToken cancellationToken)
        {
            var parentGroup = await _context.ProductGroups
            .FirstOrDefaultAsync(x => x.Id == request.Data.ParentGroupId);

            var productGroup = new ProductGroup()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Data.Name,
                Description = request.Data.Description,
                Parent = parentGroup
            };

            _context.ProductGroups.Add(productGroup);

            await _context.SaveChangesAsync();

            return new ProductGroupDto(productGroup.Id, productGroup.Name, productGroup.Description, productGroup?.Parent?.Id);

        }
    }
}
