﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YourBrand.Inventory.Domain.Entities;
using YourBrand.Inventory.Domain;
using YourBrand.Inventory.Application.Common.Models;
using MediatR;

namespace YourBrand.Inventory.Application.Features.Warehouses.Queries;

public record GetWarehousesQuery(int Page = 0, int PageSize = 10, string? SiteId = null, string? SearchString = null, string? SortBy = null, Application.Common.Models.SortDirection? SortDirection = null) : IRequest<ItemsResult<WarehouseDto>>
{
    class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, ItemsResult<WarehouseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService currentUserService;

        public GetWarehousesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<ItemsResult<WarehouseDto>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Warehouse> result = _context
                    .Warehouses
                    .OrderBy(o => o.Created)
                    .AsNoTracking()
                    .AsQueryable();

            if (request.SiteId is not null)
            {
                result = result.Where(o => o.SiteId == request.SiteId);
            }

            if (request.SearchString is not null)
            {
                result = result.Where(o => o.Name.ToLower().Contains(request.SearchString.ToLower()));
            }

            var totalCount = await result.CountAsync(cancellationToken);

            if (request.SortBy is not null)
            {
                result = result.OrderBy(request.SortBy, request.SortDirection == Application.Common.Models.SortDirection.Desc ? Inventory.Application.SortDirection.Descending : Inventory.Application.SortDirection.Ascending);
            }
            else
            {
                result = result.OrderBy(x => x.Name);
            }

            var items = await result
                .Include(x => x.Site)
                .Skip((request.Page) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new ItemsResult<WarehouseDto>(items.Select(cp => cp.ToDto()), totalCount);
        }
    }
}
