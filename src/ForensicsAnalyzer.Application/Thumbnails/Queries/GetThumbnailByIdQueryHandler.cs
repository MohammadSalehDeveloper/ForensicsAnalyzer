using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Thumbnails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Thumbnails.Queries;

public sealed class GetThumbnailByIdQueryHandler : IRequestHandler<GetThumbnailByIdQuery, ThumbnailDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetThumbnailByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ThumbnailDto?> Handle(GetThumbnailByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Thumbnails
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<ThumbnailDto>(entity);
    }
}
