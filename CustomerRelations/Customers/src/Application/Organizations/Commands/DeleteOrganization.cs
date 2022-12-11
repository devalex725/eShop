﻿using YourBrand.Customers.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace YourBrand.Customers.Application.Organizations.Commands;

public record DeleteOrganization(string OrganizationId) : IRequest
{
    public class Handler : IRequestHandler<DeleteOrganization>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrganization request, CancellationToken cancellationToken)
        {
            /*
            var invoice = await _context.Organizations
                //.Include(i => i.Addresses)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.OrganizationId, cancellationToken);

            if(invoice is null)
            {
                throw new Exception();
            }

            _context.Organizations.Remove(invoice);

            await _context.SaveChangesAsync(cancellationToken);
            */

            return Unit.Value;
        }
    }
}