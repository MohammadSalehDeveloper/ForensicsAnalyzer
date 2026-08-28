using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Queries;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<string>>
{
    private readonly IIdentityService _identityService;

    public GetUserRolesQueryHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<List<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        => await _identityService.GetUserRolesAsync(request.UserId, cancellationToken);
}
