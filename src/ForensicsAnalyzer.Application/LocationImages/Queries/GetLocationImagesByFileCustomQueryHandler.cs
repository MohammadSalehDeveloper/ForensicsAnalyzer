using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.LocationImages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.LocationImages.Queries;

public sealed class GetLocationImagesByFileCustomQueryHandler
    : IRequestHandler<GetLocationImagesByFileCustomQuery, List<LocationImageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLocationImagesByFileCustomQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LocationImageDto>> Handle(GetLocationImagesByFileCustomQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.LocationImages
            .AsNoTracking()
            .Where(li => li.FileCustomId == request.FileCustomId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LocationImageDto>>(entities);
    }
}
