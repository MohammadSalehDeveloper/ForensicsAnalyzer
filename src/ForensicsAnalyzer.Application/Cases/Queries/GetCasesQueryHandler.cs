using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Cases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, List<CaseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCasesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CaseDto>> Handle(GetCasesQuery request, CancellationToken cancellationToken)
    {
        var cases = await _context.Cases.ToListAsync(cancellationToken);
        return _mapper.Map<List<CaseDto>>(cases);
    }
}
