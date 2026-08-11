using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Locations.Queries;

public sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<LocationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLocationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<LocationDto>> Handle(GetLocationsQuery request, CancellationToken ct)
    {
        var entities = await _context.Locations.AsNoTracking()
            .Where(l => l.ArtifactId == request.ArtifactId).ToListAsync(ct);
        return _mapper.Map<List<LocationDto>>(entities);
    }
}
