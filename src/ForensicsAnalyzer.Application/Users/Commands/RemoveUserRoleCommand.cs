using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public record RemoveUserRoleCommand(string UserId, string RoleName) : IRequest;
