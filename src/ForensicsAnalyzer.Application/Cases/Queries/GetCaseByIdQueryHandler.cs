using AutoMapper;
using ForensicsAnalyzer.Application.Cases.Queries;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public sealed class GetCaseByIdQueryHandler : IRequestHandler<GetCaseByIdQuery, CaseDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCaseByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CaseDto?> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cases
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<CaseDto>(entity);
    }
}
