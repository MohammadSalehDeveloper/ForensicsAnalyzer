using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.CaseAssignments.Commands;
using ForensicsAnalyzer.Application.CaseAssignments.Queries;
using ForensicsAnalyzer.Application.Cases.Commands.AssignCase;
using ForensicsAnalyzer.Application.Cases.Commands.UnassignCase;
using ForensicsAnalyzer.Application.Cases.Queries;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/case-assignments")]
[Authorize]
public class CaseAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CaseAssignmentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [HasPermission(Permissions.Cases.Assign)]
    public async Task<ActionResult<Guid>> Create(CreateCaseAssignmentRequest request)
        => Ok(await _mediator.Send(new AssignCaseCommand(request.CaseId, request.UserId)));

    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<CaseAssignmentDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCaseAssignmentByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("by-case/{caseId:guid}")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<List<CaseAssignmentDto>>> GetByCase(Guid caseId)
        => Ok(await _mediator.Send(new GetCaseAssignmentsQuery(caseId)));

    [HttpGet("by-user/{userId}")]
    [HasPermission(Permissions.Cases.Read)]
    public async Task<ActionResult<List<CaseAssignmentDto>>> GetByUser(string userId)
        => Ok(await _mediator.Send(new GetCaseAssignmentsByUserQuery(userId)));

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Cases.Assign)]
    public async Task<IActionResult> Update(Guid id, UpdateCaseAssignmentRequest request)
    {
        await _mediator.Send(new UpdateCaseAssignmentCommand(id, request.UserId));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Cases.Assign)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new UnassignCaseCommand(id));
        return NoContent();
    }
}
