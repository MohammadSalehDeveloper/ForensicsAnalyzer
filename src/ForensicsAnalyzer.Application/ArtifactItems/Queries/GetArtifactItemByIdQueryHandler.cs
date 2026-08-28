using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.ArtifactItems.Queries;

public sealed class GetArtifactItemByIdQueryHandler : IRequestHandler<GetArtifactItemByIdQuery, ArtifactItemDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtifactItemByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ArtifactItemDto?> Handle(GetArtifactItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ArtifactItems
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<ArtifactItemDto>(entity);
    }
}
