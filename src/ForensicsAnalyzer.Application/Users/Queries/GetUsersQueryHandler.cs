using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Queries;
using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await _identityService.GetAllUsersAsync(cancellationToken);
}
