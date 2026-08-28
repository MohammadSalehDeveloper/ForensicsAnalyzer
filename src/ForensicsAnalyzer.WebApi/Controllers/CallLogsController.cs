using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.CallLogs.Commands;
using ForensicsAnalyzer.Application.CallLogs.Queries;
using ForensicsAnalyzer.Contracts.CallLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CallLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CallLogsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateCallLogCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-artifact/{artifactId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<CallLogDto>>> GetByArtifact(Guid artifactId)
        => Ok(await _mediator.Send(new GetCallLogsQuery(artifactId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<CallLogDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCallLogByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateCallLogCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCallLogCommand(id));
        return NoContent();
    }
}
