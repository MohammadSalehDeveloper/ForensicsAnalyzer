using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.FileCustoms.Commands;
using ForensicsAnalyzer.Application.FileCustoms.Queries;
using ForensicsAnalyzer.Contracts.Files;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FileCustomController : ControllerBase
{
    private readonly IMediator _mediator;
    public FileCustomController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateFileCustomCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<FileCustomDto>>> Get()
        => Ok(await _mediator.Send(new GetFileCustomsQuery()));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<FileCustomDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetFileCustomByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateFileCustomCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteFileCustomCommand(id));
        return NoContent();
    }
}
