using MediatR;

using Microsoft.EntityFrameworkCore;

using Catalog.Domain;
using Catalog.Domain.Entities;

namespace Catalog.Application.Products.Variants;

public record CreateProductVariant(string ProductId, ApiCreateProductVariant Data) : IRequest<ProductVariantDto>
{
    public class Handler : IRequestHandler<CreateProductVariant, ProductVariantDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ProductVariantsService _productVariantsService;

        public Handler(IApplicationDbContext context, ProductVariantsService productVariantsService)
        {
            _context = context;
            _productVariantsService = productVariantsService;
        }

        public async Task<ProductVariantDto> Handle(CreateProductVariant request, CancellationToken cancellationToken)
        {
            ProductVariant? match = (await _productVariantsService.FindVariantCore(request.ProductId, null, request.Data.Attributes.ToDictionary(x => x.OptionId, x => x.ValueId)!))
                .SingleOrDefault();

            if (match is not null)
            {
                throw new VariantAlreadyExistsException("Variant with the same options already exists.");
            }

            var product = await _context.Products
                .AsSplitQuery()
                .Include(pv => pv.Variants)
                    .ThenInclude(o => o.AttributeValues)
                    .ThenInclude(o => o.Attribute)
                .Include(pv => pv.Variants)
                    .ThenInclude(o => o.AttributeValues)
                    .ThenInclude(o => o.Value)
                .Include(pv => pv.Attributes)
                    .ThenInclude(o => o.Values)
                .FirstAsync(x => x.Id == request.ProductId);

            var variant = new ProductVariant()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Data.Name,
                Description = request.Data.Description,
                ItemId = request.Data.SKU,
                Price = request.Data.Price
            };

            foreach (var value in request.Data.Attributes)
            {
                var option = product.Attributes.First(x => x.Id == value.OptionId);

                var value2 = option.Values.First(x => x.Id == value.ValueId);

                variant.AttributeValues.Add(new ProductVariantAttributeValue()
                {
                    Attribute = option,
                    Value = value2
                });
            }

            product.Variants.Add(variant);

            await _context.SaveChangesAsync();

            return new ProductVariantDto(variant.Id, variant.Name, variant.Description, variant.ItemId, GetImageUrl(variant.Image), variant.Price,
                variant.AttributeValues.Select(x => x.ToDto()));
        }

        private static string? GetImageUrl(string? name)
        {
            return name is null ? null : $"http://127.0.0.1:10000/devstoreaccount1/images/{name}";
        }
    }
}
