using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CaseAssignments.Queries;

public sealed class GetCaseAssignmentsByUserQueryHandler
    : IRequestHandler<GetCaseAssignmentsByUserQuery, List<CaseAssignmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCaseAssignmentsByUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CaseAssignmentDto>> Handle(
        GetCaseAssignmentsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _context.CaseAssignments
            .AsNoTracking()
            .Where(a => a.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CaseAssignmentDto>>(entities);
    }
}
