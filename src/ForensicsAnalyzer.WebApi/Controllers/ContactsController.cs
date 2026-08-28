using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Contacts.Commands;
using ForensicsAnalyzer.Application.Contacts.Queries;
using ForensicsAnalyzer.Contracts.Contacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContactsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Artifacts.Create)]
    public async Task<ActionResult<Guid>> Create(CreateContactCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet("by-artifact/{artifactId:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<List<ContactDto>>> GetByArtifact(Guid artifactId)
        => Ok(await _mediator.Send(new GetContactsQuery(artifactId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Read)]
    public async Task<ActionResult<ContactDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetContactByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateContactCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Artifacts.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContactCommand(id));
        return NoContent();
    }
}
