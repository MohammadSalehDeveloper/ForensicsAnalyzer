using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class DeleteUserCommand : IRequest<Unit>
{
    public string Id { get; set; } = string.Empty;
}
