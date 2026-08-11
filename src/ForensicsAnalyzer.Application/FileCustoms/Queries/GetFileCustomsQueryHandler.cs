using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.FileCustoms.Queries;

public sealed class GetFileCustomsQueryHandler : IRequestHandler<GetFileCustomsQuery, List<FileCustomDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileCustomsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FileCustomDto>> Handle(GetFileCustomsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.FileCustoms.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<FileCustomDto>>(entities);
    }
}

public sealed class GetFileCustomByIdQueryHandler : IRequestHandler<GetFileCustomByIdQuery, FileCustomDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileCustomByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FileCustomDto?> Handle(GetFileCustomByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileCustoms
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<FileCustomDto>(entity);
    }
}
