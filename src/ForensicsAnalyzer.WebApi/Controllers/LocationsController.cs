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

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<LocationDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetLocationByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateLocationCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteLocationCommand(id));
        return NoContent();
    }
}
