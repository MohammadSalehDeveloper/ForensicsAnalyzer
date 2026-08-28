using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public record CreateUserCommand(
    string Email,
    string Password,
    string? FullName,
    List<string> Roles
) : IRequest<string>;
