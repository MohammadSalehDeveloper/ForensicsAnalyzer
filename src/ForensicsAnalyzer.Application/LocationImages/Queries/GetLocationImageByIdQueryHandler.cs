using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.LocationImages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.LocationImages.Queries;

public sealed class GetLocationImageByIdQueryHandler : IRequestHandler<GetLocationImageByIdQuery, LocationImageDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLocationImageByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LocationImageDto?> Handle(GetLocationImageByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.LocationImages
            .AsNoTracking()
            .FirstOrDefaultAsync(li => li.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<LocationImageDto>(entity);
    }
}
