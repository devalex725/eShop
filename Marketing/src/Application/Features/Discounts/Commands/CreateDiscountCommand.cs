﻿using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Marketing.Domain;

namespace YourBrand.Marketing.Application.Features.Discounts.Commands;

public record CreateDiscountCommand(
                double Percentage,
                decimal Amount) : IRequest<DiscountDto>
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountDto>
    {
        private readonly IApplicationDbContext context;

        public CreateDiscountCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DiscountDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = Domain.Entities.Discount.CreateDiscountForPurchase(request.Percentage, request.Amount);

            context.Discounts.Add(discount);

            await context.SaveChangesAsync(cancellationToken);

            return discount.ToDto();
        }
    }
}
