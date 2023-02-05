﻿using YourBrand.Marketing.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Marketing.Application.Features.Contacts;

namespace YourBrand.Marketing.Application.Features.Contacts.Queries;

public record GetContact(string ContactId) : IRequest<ContactDto?>
{
    public class Handler : IRequestHandler<GetContact, ContactDto?>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactDto?> Handle(GetContact request, CancellationToken cancellationToken)
        {
            var person = await _context.Contacts
                .Include(i => i.Campaign)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ContactId, cancellationToken);

            return person?.ToDto();
        }
    }
}