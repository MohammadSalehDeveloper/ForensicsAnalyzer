using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Cases.Commands;
using ForensicsAnalyzer.Application.Cases.Commands.AssignCase;
using ForensicsAnalyzer.Application.Cases.Commands.CreateCase;
using ForensicsAnalyzer.Application.Cases.Commands.UnassignCase;
using ForensicsAnalyzer.Application.Cases.Queries;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CasesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Cases.Create)]
    public async Task<ActionResult<Guid>> Create(CreateCaseCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<List<CaseDto>>> Get()
        => Ok(await _mediator.Send(new GetCasesQuery()));

    [HttpGet("mine")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<List<CaseDto>>> GetMine()
        => Ok(await _mediator.Send(new GetUserCasesQuery()));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<CaseDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCaseByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Cases.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateCaseCommand command)
    {
        if (id != command.Id)
            return BadRequest("Case ID in route does not match request payload.");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Cases.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCaseCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{caseId:guid}/assign")]
    [HasPermission(Permissions.Cases.Assign)]
    public async Task<ActionResult<Guid>> Assign(Guid caseId, AssignCaseRequest request)
        => Ok(await _mediator.Send(new AssignCaseCommand(caseId, request.UserId)));

    [HttpDelete("assignments/{assignmentId:guid}")]
    [HasPermission(Permissions.Cases.Assign)]
    public async Task<IActionResult> Unassign(Guid assignmentId)
    {
        await _mediator.Send(new UnassignCaseCommand(assignmentId));
        return NoContent();
    }

    [HttpGet("{caseId:guid}/assignments")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<List<CaseAssignmentDto>>> GetAssignments(Guid caseId)
        => Ok(await _mediator.Send(new GetCaseAssignmentsQuery(caseId)));
}
