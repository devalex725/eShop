using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using YourBrand.Analytics.Application.Statistics;
using YourBrand.Analytics.Domain.Enums;
using YourBrand.Analytics.Application.Tracking.Commands;

namespace YourBrand.Analytics.Presentation.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<string?> RegisterEvent([FromHeader(Name = "X-Client-Id")] string clientId, [FromHeader(Name = "X-Session-Id")] string sessionId, EventData dto, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new RegisterEventCommand(clientId, sessionId, dto.EventType, dto.Data), cancellationToken);
    }
}