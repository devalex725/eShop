﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;


using YourBrand.Sales.Domain.Entities;
using YourBrand.Sales.Domain;
using YourBrand.Sales.Application.Common;
using YourBrand.Sales.Application.Services;
using YourBrand.Sales.Application.Orders.Dtos;

namespace YourBrand.Sales.Application.Orders.Statuses.Queries;

public record GetOrderStatusesQuery(int Page = 0, int PageSize = 10, string? SearchString = null, string? SortBy = null, SortDirection? SortDirection = null) : IRequest<ItemsResult<OrderStatusDto>>
{
    class GetOrderStatusesQueryHandler : IRequestHandler<GetOrderStatusesQuery, ItemsResult<OrderStatusDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService currentUserService;

        public GetOrderStatusesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            this.currentUserService = currentUserService;
        }

        public async Task<ItemsResult<OrderStatusDto>> Handle(GetOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OrderStatus> result = _context
                    .OrderStatuses
                    .OrderBy(o => o.Created)
                    .AsNoTracking()
                    .AsQueryable();

            if (request.SearchString is not null)
            {
                result = result.Where(o => o.Name.ToLower().Contains(request.SearchString.ToLower()));
            }

            var totalCount = await result.CountAsync(cancellationToken);

            if (request.SortBy is not null)
            {
                result = result.OrderBy(request.SortBy, request.SortDirection);
            }
            else 
            {
                result = result.OrderBy(x => x.Name);
            }

            var items = await result
                .Skip((request.Page) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new ItemsResult<OrderStatusDto>(items.Select(cp => cp.ToDto()), totalCount);
        }
    }
}