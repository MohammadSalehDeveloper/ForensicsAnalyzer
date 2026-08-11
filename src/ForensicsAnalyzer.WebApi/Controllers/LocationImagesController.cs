using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.LocationImages.Commands;
using ForensicsAnalyzer.Application.LocationImages.Queries;
using ForensicsAnalyzer.Contracts.LocationImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LocationImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    public LocationImagesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateLocationImageCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-file/{fileCustomId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<LocationImageDto>>> GetByFile(Guid fileCustomId)
        => Ok(await _mediator.Send(new GetLocationImagesByFileCustomQuery(fileCustomId)));

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateLocationImageCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteLocationImageCommand(id));
        return NoContent();
    }
}
