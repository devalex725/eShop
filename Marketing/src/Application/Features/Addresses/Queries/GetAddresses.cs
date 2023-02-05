﻿using YourBrand.Marketing.Domain;

using MassTransit;

using MediatR;

using Microsoft.EntityFrameworkCore;

using YourBrand.Marketing.Application.Common.Models;

namespace YourBrand.Marketing.Application.Features.Addresses.Queries;

public record GetAddresses(int Page = 1, int PageSize = 10) : IRequest<ItemsResult<AddressDto>>
{
    public class Handler : IRequestHandler<GetAddresses, ItemsResult<AddressDto>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemsResult<AddressDto>> Handle(GetAddresses request, CancellationToken cancellationToken)
        {
            if (request.PageSize < 0)
            {
                throw new Exception("Page Size cannot be negative.");
            }

            if (request.PageSize > 100)
            {
                throw new Exception("Page Size must not be greater than 100.");
            }

            var query = _context.Addresses
                .AsSplitQuery()
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            int totalItems = await query.CountAsync(cancellationToken);

            query = query
                //.Include(i => i.Addresses)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize);

            var items = await query.ToArrayAsync(cancellationToken);

            return new ItemsResult<AddressDto>(
                items.Select(address => address.ToDto()),
                totalItems);
        }
    }
}