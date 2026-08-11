using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Commands;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateUserAsync(request.Id, request.FullName, request.Email, cancellationToken);
        return Unit.Value;
    }
}
