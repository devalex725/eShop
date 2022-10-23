﻿using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourBrand.Orders.Contracts;

namespace YourBrand.Orders.Presentation.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public sealed class Test : ControllerBase
{
    private readonly IPublishEndpoint publishEndpoint;

    public Test(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task UpdateStatus(int id, [FromBody] OrderStatus status, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish(new UpdateStatus(id, status), cancellationToken);
    }
}
