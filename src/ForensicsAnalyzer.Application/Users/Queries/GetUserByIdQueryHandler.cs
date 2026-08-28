using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Queries;
using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IIdentityService _identityService;

    public GetUserByIdQueryHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await _identityService.GetUserByIdAsync(request.Id, cancellationToken);
}
