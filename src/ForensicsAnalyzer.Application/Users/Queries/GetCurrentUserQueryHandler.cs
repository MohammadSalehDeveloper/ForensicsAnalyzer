using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Queries;
using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto?>
{
    private readonly IIdentityService _identityService;

    public GetCurrentUserQueryHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<UserDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        => await _identityService.GetUserByIdAsync(request.UserId, cancellationToken);
}
