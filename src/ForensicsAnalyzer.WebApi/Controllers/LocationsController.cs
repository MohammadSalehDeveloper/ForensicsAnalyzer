using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Locations.Commands;
using ForensicsAnalyzer.Application.Locations.Queries;
using ForensicsAnalyzer.Contracts.Locations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public LocationsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateLocationCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-artifact/{artifactId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<LocationDto>>> GetByArtifact(Guid artifactId)
        => Ok(await _mediator.Send(new GetLocationsQuery(artifactId)));

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteLocationCommand(id));
        return NoContent();
    }
}
