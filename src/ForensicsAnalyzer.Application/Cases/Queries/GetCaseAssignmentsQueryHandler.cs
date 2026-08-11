using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public sealed class GetCaseAssignmentsQueryHandler
    : IRequestHandler<GetCaseAssignmentsQuery, List<CaseAssignmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCaseAssignmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CaseAssignmentDto>> Handle(
        GetCaseAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.CaseAssignments
            .AsNoTracking()
            .Where(a => a.CaseId == request.CaseId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CaseAssignmentDto>>(entities);
    }
}
