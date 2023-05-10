﻿using MediatR;

using Microsoft.EntityFrameworkCore;

using YourBrand.Catalog;
using YourBrand.Catalog.Features.Stores;

namespace YourStore.Catalog.Features.Stores.Commands;

public sealed record CreateStoreCommand(string Name, string Handle, string Currency) : IRequest<StoreDto>
{
    public sealed class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, StoreDto>
    {
        private readonly IApplicationDbContext context;

        public CreateStoreCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StoreDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await context.Stores.FirstOrDefaultAsync(i => i.Name == request.Name, cancellationToken);

            if (store is not null) throw new Exception();

            var currency = await context.Currencies.FirstAsync(i => i.Code == request.Currency, cancellationToken);

            store = new YourBrand.Catalog.Domain.Entities.Store(request.Name, request.Handle, currency);

            context.Stores.Add(store);

            await context.SaveChangesAsync(cancellationToken);

            store = await context
               .Stores
               .Include(x => x.Currency)
               .AsNoTracking()
               .FirstAsync(c => c.Id == store.Id);

            return store.ToDto();
        }
    }
}
