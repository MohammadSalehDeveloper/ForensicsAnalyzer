using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class UpdateUserCommand : IRequest<Unit>
{
    public string Id { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Email { get; set; }
}
