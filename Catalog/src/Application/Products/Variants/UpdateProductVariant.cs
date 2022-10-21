using MediatR;

using Microsoft.EntityFrameworkCore;

using Catalog.Domain;
using Catalog.Domain.Entities;

namespace Catalog.Application.Products.Variants;

public record UpdateProductVariant(string ProductId, string ProductVariantId, ApiUpdateProductVariant Data) : IRequest<ProductVariantDto>
{
    public class Handler : IRequestHandler<UpdateProductVariant, ProductVariantDto>
    {
        private readonly IApplicationDbContext _context;
        private ProductVariantsService _productVariantsService;

        public Handler(IApplicationDbContext context, ProductVariantsService productVariantsService)
        {
            _context = context;
            _productVariantsService = productVariantsService;
        }
        
        public async Task<ProductVariantDto> Handle(UpdateProductVariant request, CancellationToken cancellationToken)
        {
            var match = (await _productVariantsService.FindVariantCore(request.ProductId, request.ProductVariantId, request.Data.Attributes.ToDictionary(x => x.AttributeId, x => x.ValueId)!))
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

            var variant = product.Variants.First(x => x.Id == request.ProductVariantId);

            variant.Name = request.Data.Name;
            variant.Description = request.Data.Description;
            variant.ItemId = request.Data.SKU;
            variant.Price = request.Data.Price;

            foreach (var v in request.Data.Attributes)
            {
                if (v.Id == null)
                {
                    var option = product.Attributes.First(x => x.Id == v.AttributeId);

                    var value2 = option.Values.First(x => x.Id == v.ValueId);

                    variant.AttributeValues.Add(new ProductVariantAttributeValue()
                    {
                        Attribute = option,
                        Value = value2
                    });
                }
                else
                {
                    var option = product.Attributes.First(x => x.Id == v.AttributeId);

                    var value2 = option.Values.First(x => x.Id == v.ValueId);

                    var value = variant.AttributeValues.First(x => x.Id == v.Id);

                    value.Attribute = option;
                    value.Value = value2;
                }
            }

            foreach (var v in variant.AttributeValues.ToList())
            {
                if (_context.Entry(v).State == EntityState.Added)
                    continue;

                var value = request.Data.Attributes.FirstOrDefault(x => x.Id == v.Id);

                if (value is null)
                {
                    variant.AttributeValues.Remove(v);
                }
            }

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
