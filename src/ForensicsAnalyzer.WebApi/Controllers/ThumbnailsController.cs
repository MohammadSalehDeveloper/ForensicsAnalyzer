using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Thumbnails.Commands;
using ForensicsAnalyzer.Application.Thumbnails.Queries;
using ForensicsAnalyzer.Contracts.Thumbnails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ThumbnailsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ThumbnailsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateThumbnailCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-file/{fileCustomId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<ThumbnailDto>>> GetByFile(Guid fileCustomId)
        => Ok(await _mediator.Send(new GetThumbnailsByFileCustomQuery(fileCustomId)));

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteThumbnailCommand(id));
        return NoContent();
    }
}
