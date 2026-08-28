using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Artifacts.Queries;

public sealed class GetArtifactByIdQueryHandler : IRequestHandler<GetArtifactByIdQuery, ArtifactDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtifactByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ArtifactDto?> Handle(GetArtifactByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artifacts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<ArtifactDto>(entity);
    }
}
