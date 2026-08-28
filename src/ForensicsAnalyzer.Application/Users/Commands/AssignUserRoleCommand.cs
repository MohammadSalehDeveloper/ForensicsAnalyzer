using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public record AssignUserRoleCommand(string UserId, string RoleName) : IRequest;
