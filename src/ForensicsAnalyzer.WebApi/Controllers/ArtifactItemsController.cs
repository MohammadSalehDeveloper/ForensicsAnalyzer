using ForensicsAnalyzer.Application.ArtifactItems.Commands;
using ForensicsAnalyzer.Application.ArtifactItems.Queries;
using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ArtifactItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArtifactItemsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateArtifactItemCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-artifact/{artifactId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<ArtifactItemDto>>> GetByArtifact(Guid artifactId)
        => Ok(await _mediator.Send(new GetArtifactItemsByArtifactQuery(artifactId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<ArtifactItemDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetArtifactItemByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateArtifactItemCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteArtifactItemCommand(id));
        return NoContent();
    }
}
