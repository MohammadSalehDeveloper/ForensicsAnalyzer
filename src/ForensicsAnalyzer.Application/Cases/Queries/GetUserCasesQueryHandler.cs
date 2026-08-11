using AutoMapper;
using ForensicsAnalyzer.Application.Cases.Queries;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public sealed class GetUserCasesQueryHandler : IRequestHandler<GetUserCasesQuery, List<CaseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetUserCasesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<List<CaseDto>> Handle(GetUserCasesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Cases
            .AsNoTracking()
            .Where(c => c.UserId == _currentUser.UserId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CaseDto>>(entities);
    }
}
