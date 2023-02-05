﻿
using YourBrand.Marketing.Domain;

using MediatR;
using YourBrand.Marketing.Application.Features.Contacts;
using Microsoft.EntityFrameworkCore;

namespace YourBrand.Marketing.Application.Features.Contacts.Commands;

public record CreateContact(string FirstName, string LastName, string SSN, string CampaignId) : IRequest<ContactDto>
{
    public class Handler : IRequestHandler<CreateContact, ContactDto>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactDto> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            var contact = new Domain.Entities.Contact(request.FirstName, request.LastName, request.SSN);
            contact.Campaign = await _context.Campaigns.FirstOrDefaultAsync(x => x.Id == request.CampaignId, cancellationToken);

            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync(cancellationToken);

            return contact.ToDto();
        }
    }
}
