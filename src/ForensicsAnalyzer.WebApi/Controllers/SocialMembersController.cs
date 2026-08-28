using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Social.Commands;
using ForensicsAnalyzer.Application.Social.Queries;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/social/members")]
[Authorize]
public class SocialMembersController : ControllerBase
{
    private readonly IMediator _mediator;
    public SocialMembersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Social.Create)]
    public async Task<ActionResult<Guid>> Create(CreateSocialMemberCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-chat/{chatId:guid}")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<List<SocialMemberDto>>> GetByChat(Guid chatId)
        => Ok(await _mediator.Send(new GetSocialMembersQuery(chatId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<SocialMemberDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSocialMemberByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Social.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateSocialMemberCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Social.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSocialMemberCommand(id));
        return NoContent();
    }
}
