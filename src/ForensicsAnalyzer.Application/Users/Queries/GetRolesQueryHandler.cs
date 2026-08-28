using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Users.Queries;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<string>>
{
    private readonly IIdentityService _identityService;

    public GetRolesQueryHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        => await _identityService.GetAllRolesAsync(cancellationToken);
}
