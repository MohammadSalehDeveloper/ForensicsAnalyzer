using ForensicsAnalyzer.Application.Artifacts.Commands;
using ForensicsAnalyzer.Application.Artifacts.Queries;
using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ArtifactsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArtifactsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateArtifactCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-case/{caseId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<ArtifactDto>>> GetByCase(Guid caseId)
        => Ok(await _mediator.Send(new GetArtifactsQuery(caseId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<ArtifactDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetArtifactByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateArtifactCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteArtifactCommand(id));
        return NoContent();
    }
}
