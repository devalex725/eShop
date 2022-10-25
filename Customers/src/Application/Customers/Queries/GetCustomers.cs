﻿using YourBrand.Customers.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;
using YourBrand.Customers.Application.Customers;
using YourBrand.Customers.Application.Common.Models;

namespace YourBrand.Customers.Application.Customers.Queries;

public record GetCustomers(int Page = 1, int PageSize = 10, string? SearchString = null) : IRequest<ItemsResult<CustomerDto>>
{
    public class Handler : IRequestHandler<GetCustomers, ItemsResult<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemsResult<CustomerDto>> Handle(GetCustomers request, CancellationToken cancellationToken)
        {
            if(request.PageSize < 0) 
            {
                throw new Exception("Page Size cannot be negative.");
            }

            if(request.PageSize > 100) 
            {
                throw new Exception("Page Size must not be greater than 100.");
            }

            var query = _context.Customers
                .AsSplitQuery()
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if(request.SearchString is not null) 
            {
                query = query.Where(x =>  x.Id.ToString().Contains(request.SearchString) 
                    || x.Name.ToLower().Contains(request.SearchString.ToLower())
                    || (x as Domain.Entities.Organization)!.OrganizationNo.ToLower().Contains(request.SearchString.ToLower())
                    || (x as Domain.Entities.Person)!.FirstName.ToLower().Contains(request.SearchString.ToLower())
                    || (x as Domain.Entities.Person)!.LastName.ToLower().Contains(request.SearchString.ToLower())
                    || (x as Domain.Entities.Person)!.Ssn.ToLower().Contains(request.SearchString.ToLower()));
            }

            int totalItems = await query.CountAsync(cancellationToken);

            query = query         
                //.Include(i => i.Addresses)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize);

            var items = await query.ToArrayAsync(cancellationToken);

            return new ItemsResult<CustomerDto>(
                items.Select(c => c.ToDto()),
                totalItems);
        }
    }
}