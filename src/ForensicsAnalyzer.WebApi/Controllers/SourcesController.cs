using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Sources.Commands;
using ForensicsAnalyzer.Application.Sources.Queries;
using ForensicsAnalyzer.Contracts.Sources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SourcesController : ControllerBase
{
    private readonly IMediator _mediator;
    public SourcesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Sources.Create)]
    public async Task<ActionResult<Guid>> Create(CreateSourceCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    [HasPermission(Permissions.Sources.Read)]
    public async Task<ActionResult<List<SourceDto>>> Get()
        => Ok(await _mediator.Send(new GetSourcesQuery()));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Sources.Read)]
    public async Task<ActionResult<SourceDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSourceByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Sources.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateSourceCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Sources.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSourceCommand(id));
        return NoContent();
    }
}
