using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Sources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Sources.Queries;

public sealed class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSourceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SourceDto?> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<SourceDto>(entity);
    }
}
