using ForensicsAnalyzer.Application.Interfaces;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Commands;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _identityService.FindUserByEmailAsync(request.Email, cancellationToken);
        if (existing.Succeeded)
            throw new InvalidOperationException("A user with this email already exists.");

        var result = await _identityService.CreateUserAsync(
            request.Email,
            request.Password,
            request.FullName,
            request.Roles,
            cancellationToken);

        if (!result.Succeeded || result.UserId is null)
            throw new InvalidOperationException(string.Join("; ", result.Errors));

        return result.UserId;
    }
}
