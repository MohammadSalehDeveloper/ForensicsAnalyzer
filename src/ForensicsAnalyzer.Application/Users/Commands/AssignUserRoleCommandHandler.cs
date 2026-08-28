using ForensicsAnalyzer.Application.Interfaces;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
{
    private readonly IIdentityService _identityService;

    public AssignUserRoleCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        => await _identityService.AssignRoleAsync(request.UserId, request.RoleName, cancellationToken);
}
