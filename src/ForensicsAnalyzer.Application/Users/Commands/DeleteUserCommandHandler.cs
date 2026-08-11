using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Commands;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteUserAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
