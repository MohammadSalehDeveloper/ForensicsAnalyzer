using ForensicsAnalyzer.Application.Interfaces;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand>
{
    private readonly IIdentityService _identityService;

    public RemoveUserRoleCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        => await _identityService.RemoveRoleAsync(request.UserId, request.RoleName, cancellationToken);
}
