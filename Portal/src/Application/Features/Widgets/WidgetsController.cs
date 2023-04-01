using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourBrand.Portal.Common;

namespace YourBrand.Portal.Features.Widgets;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class WidgetsController : ControllerBase
{
    private readonly IMediator mediator;

    public WidgetsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemsResult<WidgetDto>))]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesDefaultResponseType]
    public async Task<ItemsResult<WidgetDto>> GetWidgets(int page = 1, int pageSize = 10, string? sortBy = null, SortDirection? sortDirection = null, CancellationToken cancellationToken = default)
        => await mediator.Send(new GetWidgets(page, pageSize, sortBy, sortDirection), cancellationToken);

    /*
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WidgetDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<WidgetDto>> GetWidgetById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWidgetById(id), cancellationToken);
        return this.HandleResult(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WidgetDto))]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<WidgetDto>> CreateWidget(CreateWidgetRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateWidget(request.Title, request.Description, request.Status, request.AssigneeId, request.EstimatedHours, request.RemainingHours), cancellationToken);
        return result.Handle(
            onSuccess: data => CreatedAtAction(nameof(GetWidgetById), new { id = data.Id }, data),
            onError: error => Problem(detail: error.Detail, title: error.Title, type: error.Id));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteWidget(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteWidget(id), cancellationToken);
        return this.HandleResult(result);
    }
    */
}
