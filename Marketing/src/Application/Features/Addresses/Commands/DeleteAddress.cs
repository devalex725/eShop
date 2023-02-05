﻿using YourBrand.Marketing.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace YourBrand.Marketing.Application.Features.Addresses.Commands;

public record DeleteAddress(string AddressId) : IRequest
{
    public class Handler : IRequestHandler<DeleteAddress>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAddress request, CancellationToken cancellationToken)
        {
            var address = await _context.Addresses
                //.Include(i => i.Addresses)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.AddressId, cancellationToken);

            if (address is null)
            {
                throw new Exception();
            }

            _context.Addresses.Remove(address);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}