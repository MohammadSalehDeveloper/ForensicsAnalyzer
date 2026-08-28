using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Commands;
using ForensicsAnalyzer.Application.Users.Queries;
using ForensicsAnalyzer.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUser;

    public UsersController(IMediator mediator, ICurrentUserService currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    [HttpGet]
    [HasPermission(Permissions.Users.Read)]
    public async Task<ActionResult<List<UserDto>>> GetAll()
        => Ok(await _mediator.Send(new GetUsersQuery()));

    [HttpGet("me")]
    [HasPermission(Permissions.Users.Read)]
    public async Task<ActionResult<UserDto>> GetCurrent()
    {
        var result = await _mediator.Send(new GetCurrentUserQuery(_currentUser.UserId));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.Users.Read)]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [HasPermission(Permissions.Users.ManageRoles)]
    public async Task<ActionResult<string>> Create(CreateUserRequest request)
    {
        var userId = await _mediator.Send(new CreateUserCommand(
            request.Email,
            request.Password,
            request.FullName,
            request.Roles));

        return Ok(userId);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.Users.Update)]
    public async Task<IActionResult> Update(string id, UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id must match payload id.");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.Users.Delete)]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteUserCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}/roles")]
    [HasPermission(Permissions.Users.Read)]
    public async Task<ActionResult<List<string>>> GetRoles(string id)
        => Ok(await _mediator.Send(new GetUserRolesQuery(id)));

    [HttpPost("{id}/roles")]
    [HasPermission(Permissions.Users.ManageRoles)]
    public async Task<IActionResult> AssignRole(string id, AssignUserRoleRequest request)
    {
        await _mediator.Send(new AssignUserRoleCommand(id, request.RoleName));
        return NoContent();
    }

    [HttpDelete("{id}/roles/{roleName}")]
    [HasPermission(Permissions.Users.ManageRoles)]
    public async Task<IActionResult> RemoveRole(string id, string roleName)
    {
        await _mediator.Send(new RemoveUserRoleCommand(id, roleName));
        return NoContent();
    }
}
