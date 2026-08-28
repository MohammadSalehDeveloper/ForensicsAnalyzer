using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CaseAssignments.Queries;

public sealed class GetCaseAssignmentByIdQueryHandler : IRequestHandler<GetCaseAssignmentByIdQuery, CaseAssignmentDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCaseAssignmentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CaseAssignmentDto?> Handle(GetCaseAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CaseAssignments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<CaseAssignmentDto>(entity);
    }
}
