﻿
using YourBrand.Customers.Domain;

using MediatR;
using YourBrand.Customers.Application.Organizations;
using YourBrand.Customers.Application.Addresses;

namespace YourBrand.Customers.Application.Organizations.Commands;

public record CreateOrganization(string Name, string OrgNo, string? Phone, string? PhoneMobile, string? Email, Address2Dto Address) : IRequest<OrganizationDto>
{
    public class Handler : IRequestHandler<CreateOrganization, OrganizationDto>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrganizationDto> Handle(CreateOrganization request, CancellationToken cancellationToken)
        {
            var organization = new Domain.Entities.Organization(request.Name, request.OrgNo, string.Empty);
            organization.Phone = request.Phone;
            organization.PhoneMobile = request.PhoneMobile!;
            organization.Email = request.Email!;

            organization.AddAddress(new Address {
                Thoroughfare = request.Address.Thoroughfare,
                Premises = request.Address.Premises,
                SubPremises = request.Address.SubPremises,
                PostalCode = request.Address.PostalCode,
                Locality = request.Address.Locality,
                SubAdministrativeArea = request.Address.SubAdministrativeArea,
                AdministrativeArea = request.Address.AdministrativeArea,
                Country = request.Address.Country
            });

            _context.Organizations.Add(organization);

            await _context.SaveChangesAsync(cancellationToken);

            return organization.ToDto();
        }
    }
}
