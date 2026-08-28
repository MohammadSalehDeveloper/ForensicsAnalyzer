using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Social.Commands;
using ForensicsAnalyzer.Application.Social.Queries;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/social/messengers")]
[Authorize]
public class SocialMessengersController : ControllerBase
{
    private readonly IMediator _mediator;
    public SocialMessengersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Social.Create)]
    public async Task<ActionResult<Guid>> Create(CreateSocialMessengerCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<List<SocialMessengerDto>>> Get()
        => Ok(await _mediator.Send(new GetSocialMessengersQuery()));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<SocialMessengerDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSocialMessengerByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Social.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateSocialMessengerCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Social.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSocialMessengerCommand(id));
        return NoContent();
    }
}
