using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Social.Commands;
using ForensicsAnalyzer.Application.Social.Queries;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/social/chats")]
[Authorize]
public class SocialChatsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SocialChatsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Social.Create)]
    public async Task<ActionResult<Guid>> Create(CreateSocialChatCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-messenger/{messengerId:guid}")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<List<SocialChatDto>>> GetByMessenger(Guid messengerId)
        => Ok(await _mediator.Send(new GetSocialChatsQuery(messengerId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<SocialChatDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSocialChatByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Social.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateSocialChatCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Social.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSocialChatCommand(id));
        return NoContent();
    }

    [HttpGet("{chatId:guid}/messages")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<List<SocialMessageDto>>> GetMessages(Guid chatId)
        => Ok(await _mediator.Send(new GetSocialMessagesQuery(chatId)));

    [HttpGet("{chatId:guid}/members")]
    [HasPermission(Permissions.Social.Read)]
    public async Task<ActionResult<List<SocialMemberDto>>> GetMembers(Guid chatId)
        => Ok(await _mediator.Send(new GetSocialMembersQuery(chatId)));
}
