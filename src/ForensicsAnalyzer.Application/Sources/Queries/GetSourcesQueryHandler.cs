using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Sources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Sources.Queries;

public sealed class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, List<SourceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSourcesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SourceDto>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Sources.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<SourceDto>>(entities);
    }
}
