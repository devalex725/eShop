﻿using MediatR;

using Microsoft.AspNetCore.Mvc;

using YourBrand.Catalog.Application;
using YourBrand.Catalog.Application.Common.Models;
using YourBrand.Catalog.Application.Products;
using Microsoft.AspNetCore.Http;

namespace YourBrand.Catalog.Presentation.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public partial class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ItemsResult<ProductDto>>> GetProducts(
        bool includeUnlisted = false, string? groupId = null,
        int page = 0, int pageSize = 10, string? searchString = null, string? sortBy = null, Application.Common.Models.SortDirection? sortDirection = null, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetProducts(includeUnlisted, groupId, page, pageSize, searchString, sortBy, sortDirection), cancellationToken));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetProduct(string productId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetProduct(productId), cancellationToken));
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult> UpdateProductDetails(string productId, ApiUpdateProductDetails details, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateProductDetails(productId, details), cancellationToken);
        return Ok();
    }

    [HttpPost("{productId}/UploadImage")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult> UploadProductImage([FromRoute] string productId, IFormFile file, CancellationToken cancellationToken)
    {
        var url = await _mediator.Send(new UploadProductImage(productId, file.FileName, file.OpenReadStream()), cancellationToken);
        return Ok(url);
    }

    [HttpGet("{productId}/Visibility")]
    public async Task<ActionResult> UpdateProductVisibility(string productId, ProductVisibility visibility, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateProductVisibility(productId, visibility), cancellationToken);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ApiCreateProduct data, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new CreateProduct(data.Name, data.HasVariants, data.Description, data.GroupId, data.SKU, data.Price, data.Visibility), cancellationToken));
    }
}
