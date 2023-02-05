using YourBrand.Inventory.Application;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using YourBrand.Inventory.Application.Features.Warehouses.Items.Queries;
using Microsoft.AspNetCore.Authorization;
using YourBrand.Inventory.Application.Features.Warehouses.Items;
using YourBrand.Inventory.Application.Features.Warehouses.Items.Commands;
using YourBrand.Inventory.Application.Common.Models;

namespace YourBrand.Inventory.Application.Features.Warehouses.Items;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public class WarehouseItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehouseItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ItemsResult<WarehouseItemDto>> GetItems(string warehouseId, int page = 1, int pageSize = 10, string? itemId = null, string? searchString = null, string? sortBy = null, Application.Common.Models.SortDirection? sortDirection = null, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetWarehouseItems(page - 1, pageSize, warehouseId, itemId, searchString, sortBy, sortDirection), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<WarehouseItemDto?> GetItem(string warehouseId, string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetWarehouseItem(id), cancellationToken);
    }

    [HttpPost]
    public async Task<WarehouseItemDto> CreateItem(string warehouseId, CreateWarehouseItemDto dto, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AddWarehouseItem(dto.ItemId, dto.WarehouseId, dto.Location, dto.QuantityOnHand, dto.QuantityThreshold), cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task UpdateItem(string warehouseId, string id, UpdateWarehouseItemDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateWarehouseItem(id, dto.Location), cancellationToken);
    }

    [HttpPut("{id}/QuantityOnHand")]
    public async Task AdjustQuantityOnHand(string warehouseId, string id, AdjustQuantityOnHandDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AdjustQuantityOnHand(id, dto.Quantity), cancellationToken);
    }

    [HttpPut("{id}/Reserve")]
    public async Task ReserveItems(string warehouseId, string id, ReserveItemsDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReserveWarehouseItems2(warehouseId, id, dto.Quantity), cancellationToken);
    }

    [HttpPut("{id}/Pick")]
    public async Task PickItems(string warehouseId, string id, PickItemsDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new PickWarehouseItems(id, dto.Quantity, dto.FromReserved), cancellationToken);
    }

    [HttpPut("{id}/Ship")]
    public async Task ShipItems(string warehouseId, string id, ShipItemsDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ShipWarehouseItems(id, dto.Quantity, dto.FromPicked), cancellationToken);
    }

    [HttpPut("{id}/Receive")]
    public async Task ReceiveItems(string warehouseId, string id, ReceiveItemsDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReceiveWarehouseItems(id, dto.Quantity), cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task DeleteItem(string warehouseId, string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteWarehouseItem(id), cancellationToken);
    }
}

public record AdjustQuantityOnHandDto(int Quantity);

public record ReserveItemsDto(int Quantity);

public record PickItemsDto(int Quantity, bool FromReserved = false);

public record ShipItemsDto(int Quantity, bool FromPicked = false);

public record ReceiveItemsDto(int Quantity);

public record CreateWarehouseItemDto(string ItemId, string WarehouseId, string Location, int QuantityOnHand, int QuantityThreshold);

public record UpdateWarehouseItemDto(string Location);

