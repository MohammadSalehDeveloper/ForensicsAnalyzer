using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Thumbnails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Thumbnails.Queries;

public sealed class GetThumbnailsByFileCustomQueryHandler
    : IRequestHandler<GetThumbnailsByFileCustomQuery, List<ThumbnailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetThumbnailsByFileCustomQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ThumbnailDto>> Handle(GetThumbnailsByFileCustomQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Thumbnails
            .AsNoTracking()
            .Where(t => t.FileCustomId == request.FileCustomId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ThumbnailDto>>(entities);
    }
}
