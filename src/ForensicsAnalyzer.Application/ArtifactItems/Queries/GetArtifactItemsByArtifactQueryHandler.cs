using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.ArtifactItems.Queries;

public sealed class GetArtifactItemsByArtifactQueryHandler
    : IRequestHandler<GetArtifactItemsByArtifactQuery, List<ArtifactItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtifactItemsByArtifactQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ArtifactItemDto>> Handle(GetArtifactItemsByArtifactQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ArtifactItems
            .AsNoTracking()
            .Where(i => i.ArtifactId == request.ArtifactId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ArtifactItemDto>>(entities);
    }
}
