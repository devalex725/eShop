using Microsoft.AspNetCore.Mvc;

using Catalog.Application;
using Catalog.Application.Options;
using Catalog.Application.Products.Groups;
using Catalog.Application.Products.Options;
using Catalog.Application.Products.Options.Groups;

namespace Catalog.Presentation.Controllers;

partial class ProductsController : Controller
{

    [HttpGet("Groups")]
    public async Task<ActionResult<IEnumerable<ProductGroupDto>>> GetProductGroups(bool includeWithUnlistedProducts = false)
    {
        return Ok(await _mediator.Send(new GetProductGroups(includeWithUnlistedProducts)));
    }

    [HttpPost("Groups")]
    public async Task<ActionResult<ProductGroupDto>> CreateProductGroup(ApiCreateProductGroup data)
    {
        return Ok(await _mediator.Send(new CreateProductGroup(data.Name, data)));
    }

    [HttpPut("Groups/{productGroupId}")]
    public async Task<ActionResult<ProductGroupDto>> UpdateProductGroup(string productGroupId, ApiUpdateProductGroup data)
    {
        return Ok(await _mediator.Send(new UpdateProductGroup(productGroupId, data)));
    }

    [HttpDelete("Groups/{productGroupId}")]
    public async Task<ActionResult> DeleteProductGroup(string productGroupId)
    {
        await  _mediator.Send(new DeleteProductGroup(productGroupId));
        return Ok();
    }
}
