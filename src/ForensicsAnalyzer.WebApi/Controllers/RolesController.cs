using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [HasPermission(Permissions.Users.ManageRoles)]
    public async Task<ActionResult<List<string>>> GetAll()
        => Ok(await _mediator.Send(new GetRolesQuery()));
}
