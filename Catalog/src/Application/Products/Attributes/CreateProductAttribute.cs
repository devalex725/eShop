using MediatR;

using Microsoft.EntityFrameworkCore;

using YourBrand.Catalog.Application.Attributes;
using YourBrand.Catalog.Domain;
using YourBrand.Catalog.Domain.Entities;

namespace YourBrand.Catalog.Application.Products.Attributes;

public record CreateProductAttribute(string ProductId, ApiCreateProductAttribute Data) : IRequest<AttributeDto>
{
    public class Handler : IRequestHandler<CreateProductAttribute, AttributeDto>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttributeDto> Handle(CreateProductAttribute request, CancellationToken cancellationToken)
        {
            var item = await _context.Products
            .FirstAsync(attribute => attribute.Id == request.ProductId);

            var group = await _context.AttributeGroups
                .FirstOrDefaultAsync(attribute => attribute.Id == request.Data.GroupId);

            Domain.Entities.Attribute attribute = new(Guid.NewGuid().ToString())
            {
                Name = request.Data.Name,
                Description = request.Data.Description,
                Group = group,
                ForVariant = request.Data.ForVariant,
                IsMainAttribute = request.Data.IsMainAttribute
            };

            foreach (var v in request.Data.Values)
            {
                var value = new AttributeValue(Guid.NewGuid().ToString())
                {
                    Name = v.Name
                };

                attribute.Values.Add(value);
            }

            item.Attributes.Add(attribute);

            await _context.SaveChangesAsync();

            return attribute.ToDto();
        }
    }
}
