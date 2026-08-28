using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Artifacts.Queries;

public sealed class GetArtifactsQueryHandler : IRequestHandler<GetArtifactsQuery, List<ArtifactDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtifactsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ArtifactDto>> Handle(GetArtifactsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Artifacts
            .AsNoTracking()
            .Where(a => a.CaseId == request.CaseId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ArtifactDto>>(entities);
    }
}
